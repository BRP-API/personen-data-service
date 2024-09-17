using Rvig.Data.Base.Postgres.DatabaseModels;
using Rvig.Data.Base.Postgres.Helpers;
using Rvig.Data.Base.Postgres.Mappers;
using Rvig.Data.Base.Postgres.Mappers.Helpers;
using Rvig.Data.Base.Providers;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Shared.ApiModels.PersonenHistorieBase;
using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;
using Rvig.HaalCentraalApi.Shared.Exceptions;
using Rvig.HaalCentraalApi.Shared.Helpers;

namespace Rvig.Data.Personen.Mappers;
public interface IRvIGDataPersonenMapper
{
	Task<GbaPersoon> MapFrom(DbPersoonActueelWrapper dbPersoonWrapper);
	Task<IEnumerable<GbaPersoonBeperkt>> MapFrom(List<DbPersoonActueelWrapper> dbPersoonWrappers);
	Task<T> MapGbaPersoonBeperkt<T>(DbPersoonActueelWrapper dbPersoonWrapper) where T : GbaPersoonBeperkt;
}

public class RvIGDataPersonenMapper : RvIGDataMapperBase, IRvIGDataPersonenMapper
{
	private readonly ICurrentDateTimeProvider _currentDateTimeProvider;

	public RvIGDataPersonenMapper(ICurrentDateTimeProvider currentDateTimeProvider, IDomeinTabellenHelper domeinTabellenHelper) : base(domeinTabellenHelper)
	{
		_currentDateTimeProvider = currentDateTimeProvider;
	}

	public async Task<IEnumerable<GbaPersoonBeperkt>> MapFrom(List<DbPersoonActueelWrapper> dbPersoonWrappers)
	{
		return await Task.WhenAll(dbPersoonWrappers.Select(async persoon => await MapGbaPersoonBeperkt<GbaPersoonBeperkt>(persoon)));
	}

	/// <summary>
	/// Maps all attributes from the database model to the haalcentraal ingeschreven persoon model.
	/// Throws a NotImplementedException when the mapping for an ingeschreven persoon attribute is not mapped.
	/// This prevents forgetting to add mapping when updating the haalcentraal data model.
	/// </summary>
	public Task<GbaPersoon> MapFrom(DbPersoonActueelWrapper dbPersoonWrapper)
	{
		return MapGbaPersoon(dbPersoonWrapper);
	}

	public async Task<T> MapGbaPersoonBeperkt<T>(DbPersoonActueelWrapper dbPersoonWrapper) where T : GbaPersoonBeperkt
	{
		lo3_pl_persoon dbPersoon = dbPersoonWrapper.Persoon;
		T gbaPersoon = Activator.CreateInstance<T>();

		short? indicatieGeheim = dbPersoonWrapper.Inschrijving.geheim_ind;

		foreach (string propertyName in ObjectHelper.GetPropertyNames<T>())
		{
			switch (propertyName)
			{
				case nameof(GbaPersoonBeperkt.Verblijfplaats):
					gbaPersoon.Verblijfplaats = await MapVerblijfplaatsBeperkt(dbPersoonWrapper!.Verblijfplaats, dbPersoonWrapper.Adres);
					break;
				case nameof(GbaPersoonBeperkt.Geslacht):
					gbaPersoon.Geslacht = GbaMappingHelper.ParseToGeslachtEnum(dbPersoon.geslachts_aand);
					break;
				case nameof(GbaPersoonBeperkt.OpschortingBijhouding):
					gbaPersoon.OpschortingBijhouding = MapOpschortingBijhouding(dbPersoonWrapper!.Inschrijving);
					//if (dbPersoonWrapper?.Inschrijving != null)
					//{
					//	gbaPersoon.OpschortingBijhouding = new OpschortingBijhoudingBasis();
					//	MapOpschortingBijhoudingBasis(dbPersoonWrapper.Inschrijving, gbaPersoon.OpschortingBijhouding);
					//	gbaPersoon.OpschortingBijhouding = ObjectHelper.InstanceOrNullWhenDefault(gbaPersoon.OpschortingBijhouding);
					//}
					break;
				case nameof(GbaPersoon.PersoonInOnderzoek):
					gbaPersoon.PersoonInOnderzoek = MapGbaInOnderzoek(dbPersoon.onderzoek_gegevens_aand, dbPersoon.onderzoek_start_datum, dbPersoon.onderzoek_eind_datum);
					break;
				case nameof(GbaPersoonBeperkt.Naam):
					gbaPersoon.Naam = new GbaNaamBasis();
					await MapNaam(dbPersoon, gbaPersoon.Naam);
					gbaPersoon.Naam = ObjectHelper.InstanceOrNullWhenDefault(gbaPersoon.Naam);
					break;
				//case nameof(GbaPersoonBeperkt.Overlijden):
				//	if (dbPersoonWrapper?.Overlijden != null)
				//	{
				//		gbaPersoon.Overlijden = new GbaOverlijdenBeperkt();
				//		MapOverlijdenBasis(dbPersoonWrapper.Overlijden, gbaPersoon.Overlijden);
				//		gbaPersoon.Overlijden.InOnderzoek = MapGbaInOnderzoek(dbPersoonWrapper.Overlijden.onderzoek_gegevens_aand, dbPersoonWrapper.Overlijden.onderzoek_start_datum, dbPersoonWrapper.Overlijden.onderzoek_eind_datum);
				//		gbaPersoon.Overlijden = ObjectHelper.InstanceOrNullWhenDefault(gbaPersoon.Overlijden);
				//	}
				//	break;
				case nameof(GbaPersoonBeperkt.GemeenteVanInschrijving):
					if (dbPersoonWrapper?.Verblijfplaats?.inschrijving_gemeente_code != null)
					{
						gbaPersoon.GemeenteVanInschrijving = new Waardetabel
						{
							Code = dbPersoonWrapper.Verblijfplaats.inschrijving_gemeente_code?.ToString().PadLeft(4, '0'),
							Omschrijving = dbPersoonWrapper.Verblijfplaats.inschrijving_gemeente_naam ?? await _domeinTabellenHelper.GetGemeenteOmschrijving(dbPersoonWrapper.Verblijfplaats.inschrijving_gemeente_code)
						};
					}
					break;
				case nameof(GbaPersoonBeperkt.GeheimhoudingPersoonsgegevens):
					gbaPersoon.GeheimhoudingPersoonsgegevens = indicatieGeheim.HasValue && indicatieGeheim != 0 ? indicatieGeheim : null;
					break;
				case nameof(GbaPersoonBeperkt.Burgerservicenummer):
					gbaPersoon.Burgerservicenummer = dbPersoon.burger_service_nr?.ToString().PadLeft(9, '0');
					break;
				case nameof(GbaPersoonBeperkt.Geboorte):
					gbaPersoon.Geboorte = new GbaGeboorteBeperkt();
					MapGeboorteBeperkt(dbPersoon, gbaPersoon.Geboorte);
					gbaPersoon.Geboorte = ObjectHelper.InstanceOrNullWhenDefault(gbaPersoon.Geboorte);
					break;
				case nameof(GbaPersoonBeperkt.Rni):
					gbaPersoon.Rni = await MapRniDeelnemers(dbPersoonWrapper!);
					break;
				case nameof(GbaPersoonBeperkt.Verificatie):
					gbaPersoon.Verificatie = MapVerificatie(dbPersoonWrapper!.Inschrijving);
					break;
				case nameof(GbaGezagPersoonBeperkt.Gezag):
					//	gbaPersoon.Gezag = //MapVerificatie(dbPersoonWrapper!.Inschrijving);
					break;
				case nameof(GbaGezagPersoonBeperkt.Partners):
					(List<GbaPartner>? currentPartners, List<GbaPartner>? historicPartners) = await GetPartners(dbPersoonWrapper!.Partners);
					gbaPersoon.Partners = currentPartners;
					gbaPersoon.HistorischePartners = historicPartners;
					break;
				case nameof(GbaPersoon.HistorischePartners):
					// Handled in mapping of partners
					break;
				case nameof(GbaPersoon.Kinderen):
					gbaPersoon.Kinderen = (await Task.WhenAll(dbPersoonWrapper!.Kinderen
						.Where(x => x.onjuist_ind == null)
						.OrderBy(x => x.stapel_nr)
						.Select(async x => await MapKind(x))))
						.Where(x => x != null)
						.Cast<GbaKind>()
						.ToList();
					break;
				case nameof(GbaPersoon.Ouders):
					gbaPersoon.Ouders = new List<GbaOuder?> { await MapOuder(dbPersoonWrapper?.Ouder1), await MapOuder(dbPersoonWrapper?.Ouder2) }
						.Where(x => x != null).Cast<GbaOuder>().ToList();
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaPersoonBeperkt)} property {propertyName}.");
			}
		}

		return gbaPersoon;
	}

	private async Task<GbaPersoon> MapGbaPersoon(DbPersoonActueelWrapper dbPersoonWrapper)
	{
		lo3_pl_persoon dbPersoon = dbPersoonWrapper.Persoon;
		GbaPersoon gbaPersoon = new();
		short? indicatieGeheim = dbPersoonWrapper.Inschrijving.geheim_ind;

		foreach (string propertyName in ObjectHelper.GetPropertyNames<GbaPersoon>())
		{
			await MapGbaPersoonProperty(dbPersoonWrapper, dbPersoon, gbaPersoon, indicatieGeheim, propertyName);
		}

		return gbaPersoon;
	}

	private async Task MapGbaPersoonProperty(DbPersoonActueelWrapper dbPersoonWrapper, lo3_pl_persoon dbPersoon, GbaPersoon gbaPersoon, short? indicatieGeheim, string propertyName)
	{
		switch (propertyName)
		{
			case nameof(GbaPersoon.Kinderen):
				gbaPersoon.Kinderen = (await Task.WhenAll(dbPersoonWrapper.Kinderen
					.Where(x => x.onjuist_ind == null)
					.OrderBy(x => x.stapel_nr)
					.Select(async x => await MapKind(x))))
					.Where(x => x != null)
					.Cast<GbaKind>()
					//.OrderByDescending(x => x.Geboorte?.Datum != null).
					//		ThenBy(x => x.Geboorte?.DatumJaar).
					//		ThenBy(x => x.Geboorte?.DatumMaand).
					//		ThenBy(x => x.Geboorte?.DatumDag).
					//		ThenBy(x => x.Naam?.Voornamen)
					.ToList();
				break;
			case nameof(GbaPersoon.Ouders):
				gbaPersoon.Ouders = new List<GbaOuder?> { await MapOuder(dbPersoonWrapper?.Ouder1), await MapOuder(dbPersoonWrapper?.Ouder2) }
					.Where(x => x != null).Cast<GbaOuder>().ToList();
				break;
			case nameof(GbaPersoon.HistorischePartners):
				// Handled in mapping of partners
				break;
			case nameof(GbaPersoon.Partners):
				(List<GbaPartner>? currentPartners, List<GbaPartner>? historicPartners) = await GetPartners(dbPersoonWrapper.Partners);
				gbaPersoon.Partners = currentPartners;
				gbaPersoon.HistorischePartners = historicPartners;
				break;
			case nameof(GbaPersoon.ANummer):
				gbaPersoon.ANummer = dbPersoon.a_nr?.ToString().PadLeft(10, '0');
				break;
			case nameof(GbaPersoon.Geslacht):
				gbaPersoon.Geslacht = GbaMappingHelper.ParseToGeslachtEnum(dbPersoon.geslachts_aand);
				break;
			case nameof(GbaPersoon.OpschortingBijhouding):
				gbaPersoon.OpschortingBijhouding = MapOpschortingBijhouding(dbPersoonWrapper!.Inschrijving);
				break;
			case nameof(GbaPersoon.DatumEersteInschrijvingGBA):
				gbaPersoon.DatumEersteInschrijvingGBA = GbaMappingHelper.ParseToDatumOnvolledig(dbPersoonWrapper?.Inschrijving.gba_eerste_inschrijving_datum);
				break;
			case nameof(GbaPersoon.UitsluitingKiesrecht):
				if (!string.IsNullOrWhiteSpace(dbPersoonWrapper?.Inschrijving.kiesrecht_uitgesl_aand) && dbPersoonWrapper.Inschrijving.kiesrecht_uitgesl_aand == "A")
				{
					gbaPersoon.UitsluitingKiesrecht = MapUitsluitingKiesrecht(dbPersoonWrapper.Inschrijving);
				}
				break;
			case nameof(GbaPersoon.EuropeesKiesrecht):
				if (dbPersoonWrapper?.Inschrijving.europees_kiesrecht_aand.HasValue == true && (dbPersoonWrapper.Inschrijving.europees_kiesrecht_aand == 1 || dbPersoonWrapper.Inschrijving.europees_kiesrecht_aand == 2))
				{
					gbaPersoon.EuropeesKiesrecht = MapEuropeesKiesrecht(dbPersoonWrapper.Inschrijving);
				}
				break;
			case nameof(GbaPersoon.Naam):
				gbaPersoon.Naam = await MapNaamPersoon(dbPersoon);
				break;
			case nameof(GbaPersoon.PersoonInOnderzoek):
				gbaPersoon.PersoonInOnderzoek = MapGbaInOnderzoek(dbPersoon.onderzoek_gegevens_aand, dbPersoon.onderzoek_start_datum, dbPersoon.onderzoek_eind_datum);
				break;
			case nameof(GbaPersoon.Nationaliteiten):
				gbaPersoon.Nationaliteiten = (await MapNationaliteiten<GbaNationaliteit>(dbPersoonWrapper!.Nationaliteiten))?.ToList();
				break;
			case nameof(GbaPersoon.Overlijden):
				gbaPersoon.Overlijden = await MapOverlijden(dbPersoonWrapper!.Overlijden);
				break;
			case nameof(GbaPersoon.Verblijfplaats):
				if (dbPersoonWrapper?.Verblijfplaats != null && dbPersoonWrapper?.Adres != null)
				{
					gbaPersoon.GemeenteVanInschrijving ??= await GetGemeenteVanInschrijving(dbPersoonWrapper!.Verblijfplaats);
					gbaPersoon.Verblijfplaats = await MapVerblijfplaats<GbaVerblijfplaats>(dbPersoonWrapper!.Verblijfplaats, dbPersoonWrapper!.Adres, gbaPersoon.GemeenteVanInschrijving);
				}
				break;
			case nameof(GbaPersoon.Immigratie):
				gbaPersoon.Immigratie = await MapImmigratie(dbPersoonWrapper!.Verblijfplaats);
				break;
			case nameof(GbaPersoon.GemeenteVanInschrijving):
				if (dbPersoonWrapper?.Verblijfplaats != null && gbaPersoon.GemeenteVanInschrijving == null)
				{
					gbaPersoon.GemeenteVanInschrijving = await GetGemeenteVanInschrijving(dbPersoonWrapper!.Verblijfplaats);
				}
				break;
			case nameof(GbaPersoon.DatumInschrijvingInGemeente):
				if (dbPersoonWrapper?.Verblijfplaats != null)
				{
					gbaPersoon.DatumInschrijvingInGemeente = GbaMappingHelper.ParseToDatumOnvolledig(dbPersoonWrapper?.Verblijfplaats.inschrijving_datum);
				}
				break;
			case nameof(GbaPersoon.IndicatieCurateleRegister):
				if (dbPersoonWrapper?.Gezagsverhouding != null)
				{
					gbaPersoon.IndicatieCurateleRegister = dbPersoonWrapper?.Gezagsverhouding.curatele_register_ind == 1 ? true : null;
				}
				break;
			case nameof(GbaPersoon.GezagInOnderzoek):
				if (dbPersoonWrapper?.Gezagsverhouding != null)
				{
					gbaPersoon.GezagInOnderzoek = MapGbaInOnderzoek(dbPersoonWrapper?.Gezagsverhouding.onderzoek_gegevens_aand, dbPersoonWrapper?.Gezagsverhouding.onderzoek_start_datum, dbPersoonWrapper?.Gezagsverhouding.onderzoek_eind_datum);
				}
				break;
			case nameof(GbaPersoon.IndicatieGezagMinderjarige):
				gbaPersoon.IndicatieGezagMinderjarige = !string.IsNullOrWhiteSpace(dbPersoonWrapper?.Gezagsverhouding?.minderjarig_gezag_ind) ? new Waardetabel
				{
					Code = dbPersoonWrapper.Gezagsverhouding.minderjarig_gezag_ind,
					Omschrijving = dbPersoonWrapper.Gezagsverhouding.minderjarig_gezag_oms ?? await _domeinTabellenHelper.GetGezagsverhoudingOmschrijving(dbPersoonWrapper.Gezagsverhouding.minderjarig_gezag_ind),
				} : null;
				break;
			case nameof(GbaPersoon.Verblijfstitel):
				var verblijfstitel = await MapVerblijfstitel(dbPersoonWrapper!.Verblijfstitel);
				gbaPersoon.Verblijfstitel = GbaMappingHelper.IsDatumOnvolledigNullOrFutureDate(verblijfstitel?.DatumEinde, _currentDateTimeProvider)
													&& verblijfstitel?.Aanduiding?.Code?.Equals("98") == false
												? verblijfstitel
												: null;
				break;
			case nameof(GbaPersoon.GeheimhoudingPersoonsgegevens):
				gbaPersoon.GeheimhoudingPersoonsgegevens = indicatieGeheim.HasValue && indicatieGeheim != 0 ? indicatieGeheim : null;
				break;
			case nameof(GbaPersoon.Burgerservicenummer):
				gbaPersoon.Burgerservicenummer = dbPersoon.burger_service_nr?.ToString().PadLeft(9, '0');
				break;
			case nameof(GbaPersoon.Geboorte):
				gbaPersoon.Geboorte = await MapGeboorte(dbPersoon);
				break;
			case nameof(GbaPersoonBeperkt.Rni):
				gbaPersoon.Rni = await MapRniDeelnemers(dbPersoonWrapper);
				break;
			case nameof(GbaPersoon.Verificatie):
				gbaPersoon.Verificatie = MapVerificatie(dbPersoonWrapper.Inschrijving);
				break;
			case nameof(GbaPersoon.Gezag):
				//	gbaPersoon.Gezag = //MapVerificatie(dbPersoonWrapper!.Inschrijving);
				break;

			default:
				throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaPersoon)} property {propertyName}.");
		}
	}

	private async Task<(List<GbaPartner>? currentPartners, List<GbaPartner>? historicPartners)> GetPartners(List<lo3_pl_persoon> dbPartners)
	{
		(List<GbaPartner>? currentPartners, List<GbaPartner>? historicPartners) = (new List<GbaPartner>(), new List<GbaPartner>());
		if (dbPartners?.Any() == false)
		{
			return (currentPartners, historicPartners);
		}
		IEnumerable<GbaPartner?> hcCurrentPartnersAndModels = await MapPartners<GbaPartner>(dbPartners!);

		List<GbaPartner> currentPartnersOrdered = hcCurrentPartnersAndModels.Select(x => x)
			.Where(x => x != null)
			.Cast<GbaPartner>()
				.OrderByDescending(x => x?.AangaanHuwelijkPartnerschap?.Datum != null).
					ThenByDescending(x => x?.AangaanHuwelijkPartnerschap?.DatumJaar).
					ThenByDescending(x => x?.AangaanHuwelijkPartnerschap?.DatumMaand).
					ThenByDescending(x => x?.AangaanHuwelijkPartnerschap?.DatumDag)
				.OrderByDescending(x => x?.OntbindingHuwelijkPartnerschap?.Datum != null).
					ThenByDescending(x => x?.OntbindingHuwelijkPartnerschap?.DatumJaar).
					ThenByDescending(x => x?.OntbindingHuwelijkPartnerschap?.DatumMaand).
					ThenByDescending(x => x?.OntbindingHuwelijkPartnerschap?.DatumDag).
					ThenBy(x => x?.Naam?.Voornamen).ToList();

		if (currentPartnersOrdered?.Any() == false)
		{
			return (currentPartners, historicPartners);
		}
		// If partners contain only expired partners then remove all except the latest partner.
		else if (currentPartnersOrdered?.All(partner => partner.OntbindingHuwelijkPartnerschap != null) == true)
		{
			var actualPartner = currentPartnersOrdered.First();
			historicPartners = currentPartnersOrdered.Where(partner => partner != actualPartner).ToList();
			currentPartnersOrdered?.RemoveRange(1, currentPartnersOrdered.Count - 1);
		}
		// If partners contains both current and expired partner items then remove ALL expired partners.
		else if (currentPartnersOrdered?.Any(partner => partner.OntbindingHuwelijkPartnerschap != null) == true
			&& currentPartnersOrdered?.All(partner => partner.OntbindingHuwelijkPartnerschap != null) == false)
		{
			historicPartners = currentPartnersOrdered.Where(partner => partner.OntbindingHuwelijkPartnerschap != null).ToList() ?? new List<GbaPartner>();
			currentPartnersOrdered = currentPartnersOrdered.Except(historicPartners).ToList();
		}

		currentPartners = currentPartnersOrdered?.ToList();
		return (currentPartners, historicPartners);
	}

	private async Task<GbaNaamPersoon?> MapNaamPersoon(lo3_pl_persoon dbPersoon)
	{
		var naamPersoon = new GbaNaamPersoon();

		await MapNaam(dbPersoon, naamPersoon);

		foreach (string propertyName in ObjectHelper.GetPropertyNames<GbaNaamPersoon>())
		{
			switch (propertyName)
			{
				case nameof(GbaNaamPersoon.AanduidingNaamgebruik):
					naamPersoon.AanduidingNaamgebruik = GbaMappingHelper.ParseToNaamgebruikEnum(dbPersoon.naam_gebruik_aand);
					break;

				// Naam properties are mapped with MapNaam
				case nameof(GbaNaamPersoon.Voornamen):
				case nameof(GbaNaamPersoon.Geslachtsnaam):
				case nameof(GbaNaamPersoon.Voorvoegsel):
				case nameof(GbaNaamPersoon.AdellijkeTitelPredicaat):
					break;

				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaNaamPersoon)} property {propertyName}.");
			}
		}

		return ObjectHelper.InstanceOrNullWhenDefault(naamPersoon);
	}

	// , int? indicatieGeheim
	private async Task<GbaOuder?> MapOuder(lo3_pl_persoon? dbOuder)
	{
		// Because of GBA feature, the GBA API must always return an empty GbaOuder object
		//if (dbOuder?.geslachts_naam == null && dbOuder?.voor_naam == null && dbOuder?.geboorte_datum == null)
		//    return null;

		var ouder = new GbaOuder();

		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaOuder>())
		{
			switch (propertyName)
			{
				case nameof(GbaOuder.Geslacht):
					ouder.Geslacht = GbaMappingHelper.ParseToGeslachtEnum(dbOuder?.geslachts_aand);
					break;
				case nameof(GbaOuder.OuderAanduiding):
					ouder.OuderAanduiding = dbOuder?.persoon_type;
					break;
				case nameof(GbaOuder.DatumIngangFamilierechtelijkeBetrekking):
					ouder.DatumIngangFamilierechtelijkeBetrekking = GbaMappingHelper.ParseToDatumOnvolledig(dbOuder?.familie_betrek_start_datum);
					break;
				case nameof(GbaOuder.Naam):
					ouder.Naam = dbOuder == null ? null : await MapNaam(dbOuder, new GbaNaamBasis());
					break;
				case nameof(GbaOuder.InOnderzoek):
					ouder.InOnderzoek = MapGbaInOnderzoek(dbOuder?.onderzoek_gegevens_aand, dbOuder?.onderzoek_start_datum, dbOuder?.onderzoek_eind_datum);
					break;
				case nameof(GbaOuder.Burgerservicenummer):
					ouder.Burgerservicenummer = dbOuder?.burger_service_nr?.ToString().PadLeft(9, '0');
					break;
				case nameof(GbaOuder.Geboorte):
					ouder.Geboorte = dbOuder == null ? null : await MapGeboorte(dbOuder);
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaOuder)} property {propertyName}.");
			}
		}

		// Because of GBA feature, the GBA API must always return an empty GbaOuder object
		//return ObjectHelper.InstanceOrNullWhenDefault(ouder);

		if (IsOuderNonExistent(ouder))
		{
			return null;
		}

		return ouder;
	}

	// This has to do with vondeling logic. Below is the rule of Haal Centraal written in an ouder-gba.feature.
	// Wanneer alleen gegevens in groep 81, 82, 83, 84, 85 en/of 86 zijn opgenomen en geen gegevens in groep 1, 2, 3, 4 of 62, dan wordt de ouder niet opgenomen
	private bool IsOuderNonExistent(GbaOuder ouder)
	{
		return string.IsNullOrWhiteSpace(ouder.Burgerservicenummer) && ouder.Naam  == null
			&& ouder.Geboorte == null && ouder.Geslacht == null
			&& string.IsNullOrWhiteSpace(ouder.DatumIngangFamilierechtelijkeBetrekking);
	}

	// , int? indicatieGeheim
	private async Task<GbaKind?> MapKind(lo3_pl_persoon dbkind)
	{
		var kind = new GbaKind();

		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaKind>())
		{
			switch (propertyName)
			{
				case nameof(GbaKind.InOnderzoek):
					kind.InOnderzoek = MapGbaInOnderzoek(dbkind.onderzoek_gegevens_aand, dbkind.onderzoek_start_datum, dbkind.onderzoek_eind_datum);
					break;
				case nameof(GbaKind.Naam):
					kind.Naam = await MapNaam(dbkind, new GbaNaamBasis());
					break;
				case nameof(GbaKind.Burgerservicenummer):
					kind.Burgerservicenummer = dbkind.burger_service_nr?.ToString().PadLeft(9, '0');
					break;
				case nameof(GbaKind.Geboorte):
					kind.Geboorte = await MapGeboorte(dbkind);
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaKind)} property {propertyName}.");
			}
		}

		// Because of GBA feature, the GBA API must always return an empty GbaKind object
		//return ObjectHelper.InstanceOrNullWhenDefault(kind);

		if (IsKindNonExistent(kind))
		{
			return null;
		}

		return kind;
	}

	// This has to do with vondeling logic. Below is the rule of Haal Centraal written in an ouder-gba.feature.
	// Een kind wordt alleen teruggegeven als minimaal één gegeven in de identificatienummers (groep 01), naam (groep 02) of geboorte (groep 03) van het kind een waarde heeft.
    // - Wanneer in een categorie kind alleen gegevens zijn opgenomen in groep 81 of 82, 85 en 86, wordt dit kind niet opgenomen in het antwoord
    // - Wanneer een gegeven een standaardwaarde heeft(dit betekent dat de waarde onbekend is), zoals "." (punt) bij geslachtsnaam of "00000000" bij geboortedatum, geldt dat hier als het bestaan van een waarde en wordt het kind wel geleverd.
    // - Wanneer door de gebruikte fields parameter in het request het kind in de response geen enkel gegeven heeft met een waarde, dan wordt het kind geleverd zonder gegevens (dus als leeg object)
	private bool IsKindNonExistent(GbaKind kind)
	{
		return string.IsNullOrWhiteSpace(kind.Burgerservicenummer) && kind.Naam == null
			&& kind.Geboorte == null;
	}

	private GbaEuropeesKiesrecht? MapEuropeesKiesrecht(lo3_pl inschrijving)
	{
		var europeesKiesrecht = new GbaEuropeesKiesrecht();

		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaEuropeesKiesrecht>())
		{
			switch (propertyName)
			{
				case nameof(GbaEuropeesKiesrecht.Aanduiding):
					europeesKiesrecht.Aanduiding = new Waardetabel
					{
						Code = inschrijving.europees_kiesrecht_aand.ToString(),
						// Only support 1 and 2.
						Omschrijving = inschrijving.europees_kiesrecht_aand == 1 ? "persoon is uitgesloten" : "persoon ontvangt oproep"
					};
					break;
				case nameof(GbaEuropeesKiesrecht.EinddatumUitsluiting):
					if (inschrijving.europees_kiesrecht_aand == 1)
					{
						var datumEinde = GbaMappingHelper.ParseToDatumOnvolledig(inschrijving.europees_uitsluit_eind_datum);

						if (GbaMappingHelper.IsDatumOnvolledigNullOrFutureDate(datumEinde, _currentDateTimeProvider))
						{
							europeesKiesrecht.EinddatumUitsluiting = datumEinde;
						}
						else
						{
							return ObjectHelper.InstanceOrNullWhenDefault(new GbaEuropeesKiesrecht());
						}
					}
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaEuropeesKiesrecht)} property {propertyName}.");
			}
		}

		return ObjectHelper.InstanceOrNullWhenDefault(europeesKiesrecht);
	}

	private GbaUitsluitingKiesrecht? MapUitsluitingKiesrecht(lo3_pl inschrijving)
	{
		var uitsluitingKiesrecht = new GbaUitsluitingKiesrecht
		{
			UitgeslotenVanKiesrecht = true
		};

		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaUitsluitingKiesrecht>())
		{
			switch (propertyName)
			{
				case nameof(GbaUitsluitingKiesrecht.UitgeslotenVanKiesrecht):
					// Standardly set.
					break;
				case nameof(GbaUitsluitingKiesrecht.Einddatum):
					var datum = GbaMappingHelper.ParseToDatumOnvolledig(inschrijving.kiesrecht_uitgesl_eind_datum);

					if (GbaMappingHelper.IsDatumOnvolledigNullOrFutureDate(datum, _currentDateTimeProvider))
					{
						uitsluitingKiesrecht.Einddatum = datum;
					}
					else
					{
						return ObjectHelper.InstanceOrNullWhenDefault(new GbaUitsluitingKiesrecht());
					}
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaUitsluitingKiesrecht)} property {propertyName}.");
			}
		}

		return ObjectHelper.InstanceOrNullWhenDefault(uitsluitingKiesrecht);
	}

	private async Task<GbaOverlijden?> MapOverlijden(lo3_pl_overlijden dbOverlijden)
	{
		var overlijden = new GbaOverlijden();
		if (dbOverlijden == null)
		{
			return null;
		}

		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaOverlijden>())
		{
			switch (propertyName)
			{
				case nameof(GbaOverlijden.Plaats):
					overlijden.Plaats = await MapPlaats(dbOverlijden.overlijden_land_code, dbOverlijden.overlijden_plaats, dbOverlijden.overlijden_plaats_naam);
					break;
				case nameof(GbaOverlijden.Land):
					if (dbOverlijden.overlijden_land_code.HasValue)
					{
						overlijden.Land =  new Waardetabel {
							Code = dbOverlijden.overlijden_land_code?.ToString().PadLeft(4, '0'),
							Omschrijving = dbOverlijden.overlijden_land_naam ?? await _domeinTabellenHelper.GetLandOmschrijving(dbOverlijden.overlijden_land_code)
						};
					}
					break;
				case nameof(GbaOverlijden.InOnderzoek):
					overlijden.InOnderzoek = MapGbaInOnderzoek(dbOverlijden.onderzoek_gegevens_aand, dbOverlijden.onderzoek_start_datum, dbOverlijden.onderzoek_eind_datum);
					break;
				case nameof(GbaOverlijden.Datum):
					overlijden.Datum = GbaMappingHelper.ParseToDatumOnvolledig(dbOverlijden.overlijden_datum);
					break;

				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaOverlijden)} property {propertyName}.");
			}
		}

		return ObjectHelper.InstanceOrNullWhenDefault(overlijden);
	}

	private async Task<GbaImmigratie?> MapImmigratie(lo3_pl_verblijfplaats dbVerblijfplaats)
	{
		var immigratie = new GbaImmigratie();
		if (dbVerblijfplaats == null)
		{
			return null;
		}

		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaImmigratie>())
		{
			switch (propertyName)
			{
				case nameof(GbaImmigratie.DatumVestigingInNederland):
					immigratie.DatumVestigingInNederland = GbaMappingHelper.ParseToDatumOnvolledig(dbVerblijfplaats.vestiging_datum);
					break;
				case nameof(GbaImmigratie.LandVanwaarIngeschreven):
					if (dbVerblijfplaats.vestiging_land_code != null)
					{
						immigratie.LandVanwaarIngeschreven = new Waardetabel {
							Code = dbVerblijfplaats.vestiging_land_code?.ToString().PadLeft(4, '0'),
							Omschrijving = dbVerblijfplaats.vestiging_land_naam ?? await _domeinTabellenHelper.GetLandOmschrijving(dbVerblijfplaats.vestiging_land_code)
						};
					}
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaImmigratie)} property {propertyName}.");
			}
		}

		return ObjectHelper.InstanceOrNullWhenDefault(immigratie);
	}

	private async Task<GbaVerblijfplaatsBeperkt?> MapVerblijfplaatsBeperkt(lo3_pl_verblijfplaats dbVerblijfplaats, lo3_adres dbAdres)
	{
		var verblijfplaats = new GbaVerblijfplaatsBeperkt();
		if (dbVerblijfplaats == null && dbAdres == null)
		{
			return null;
		}

		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaVerblijfplaatsBeperkt>())
		{
			switch (propertyName)
			{
				// Was removed
				//case nameof(GbaVerblijfplaatsBeperkt.FunctieAdres):
				//	verblijfplaats.FunctieAdres = PersoonMappingHelper.ParseToSoortAdresEnum(dbVerblijfplaats!.adres_functie);
				//	break;
				case nameof(GbaVerblijfplaatsBeperkt.Locatiebeschrijving):
					verblijfplaats.Locatiebeschrijving = dbAdres?.diak_locatie_beschrijving ?? dbAdres?.locatie_beschrijving;
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Straat):
					verblijfplaats.Straat = dbAdres?.diak_straat_naam ?? dbAdres?.straat_naam;
					break;
				case nameof(GbaVerblijfplaatsBeperkt.AanduidingBijHuisnummer):
					verblijfplaats.AanduidingBijHuisnummer = GbaMappingHelper.ParseToAanduidingBijHuisnummerEnum(dbAdres?.huis_nr_aand);
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Regel1):
					verblijfplaats.Regel1 = dbVerblijfplaats?.vertrek_land_adres_1;
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Regel2):
					verblijfplaats.Regel2 = dbVerblijfplaats?.vertrek_land_adres_2;
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Regel3):
					verblijfplaats.Regel3 = dbVerblijfplaats?.vertrek_land_adres_3;
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Land):
                    if (dbVerblijfplaats?.vertrek_land_code != null)
                    {
                        verblijfplaats.Land = new Waardetabel
                        {
                            Code = (dbVerblijfplaats.vertrek_land_code ?? 0).ToString("0000"),
                            Omschrijving = dbVerblijfplaats.vertrek_land_naam ?? await _domeinTabellenHelper.GetLandOmschrijving(dbVerblijfplaats.vertrek_land_code)
                        };
                    }
                    break;
                case nameof(GbaVerblijfplaatsBeperkt.Huisnummer):
					verblijfplaats.Huisnummer = dbAdres?.huis_nr;
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Huisletter):
					verblijfplaats.Huisletter = dbAdres?.huis_letter;
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Huisnummertoevoeging):
					verblijfplaats.Huisnummertoevoeging = dbAdres?.huis_nr_toevoeging;
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Postcode):
					verblijfplaats.Postcode = dbAdres?.postcode;
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Woonplaats):
					verblijfplaats.Woonplaats = !string.IsNullOrEmpty(dbAdres?.diak_woon_plaats_naam) ? dbAdres?.diak_woon_plaats_naam : dbAdres?.woon_plaats_naam;
					break;
				case nameof(GbaVerblijfplaatsBeperkt.InOnderzoek):
					verblijfplaats.InOnderzoek = MapGbaInOnderzoek(dbVerblijfplaats?.onderzoek_gegevens_aand, dbVerblijfplaats?.onderzoek_start_datum, dbVerblijfplaats?.onderzoek_eind_datum);
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaVerblijfplaatsBeperkt)} property {propertyName}.");
			}
		}

		return ObjectHelper.InstanceOrNullWhenDefault(verblijfplaats);
	}

	protected GbaVerificatie? MapVerificatie(lo3_pl inschrijving)
	{
		var verificatie = new GbaVerificatie();

		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaVerificatie>())
		{
			switch (propertyName)
			{
				case nameof(GbaVerificatie.Omschrijving):
					verificatie.Omschrijving = inschrijving.verificatie_oms;
					break;
				case nameof(GbaVerificatie.Datum):
					verificatie.Datum = GbaMappingHelper.ParseToDatumOnvolledig(inschrijving.verificatie_datum);
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaVerificatie)} property {propertyName}.");
			}
		}

		return ObjectHelper.InstanceOrNullWhenDefault(verificatie);
	}

	protected async Task<List<RniDeelnemer>?> MapRniDeelnemers(DbPersoonActueelWrapper dbPersoonWrapper)
	{
		var rniDeelnemerDictionary = new Dictionary<string, List<(short deelnemerCode, string? deelnemerOmschrijving, string? omschrijvingVerdrag)>>();

		if (dbPersoonWrapper.Persoon != null
			&& dbPersoonWrapper.Persoon.rni_deelnemer != null)
		{
			rniDeelnemerDictionary.Add("Persoon", new List<(short deelnemerCode, string? deelnemerOmschrijving, string? omschrijvingVerdrag)> { (dbPersoonWrapper.Persoon.rni_deelnemer.Value, dbPersoonWrapper.Persoon.pers_rni_deelnemer_omschrijving ?? await _domeinTabellenHelper.GetRniDeelnemerOmschrijving(dbPersoonWrapper.Persoon.rni_deelnemer), dbPersoonWrapper.Persoon.verdrag_oms) });
		}
		if (dbPersoonWrapper.Nationaliteiten != null
			&& dbPersoonWrapper.Nationaliteiten.Any(nat => nat.rni_deelnemer != null))
		{
			var natRniDeelnemers = new List<(short deelnemerCode, string? deelnemerOmschrijving, string? omschrijvingVerdrag)>();

			// nat => nat.rni_deelnemer != null has been done so it cannot be a null anymore.
			foreach(var nat in dbPersoonWrapper.Nationaliteiten.Where(nat => nat.rni_deelnemer != null && !nat.nl_nat_verlies_reden.HasValue && string.IsNullOrWhiteSpace(nat.nl_nat_verlies_reden_oms)))
			{
				natRniDeelnemers.Add((nat.rni_deelnemer!.Value, nat.rni_deelnemer_omschrijving ?? await _domeinTabellenHelper.GetRniDeelnemerOmschrijving(nat.rni_deelnemer), nat.verdrag_oms));
			}
			if (natRniDeelnemers.Any())
			{
				rniDeelnemerDictionary.Add("Nationaliteit", natRniDeelnemers);
			}
		}
		if (dbPersoonWrapper.Overlijden != null
			&& dbPersoonWrapper.Overlijden.rni_deelnemer != null)
		{
			rniDeelnemerDictionary.Add("Overlijden", new List<(short deelnemerCode, string? deelnemerOmschrijving, string? omschrijvingVerdrag)> { (dbPersoonWrapper.Overlijden.rni_deelnemer.Value, dbPersoonWrapper.Overlijden.overlijden_rni_deelnemer_omschrijving ?? await _domeinTabellenHelper.GetRniDeelnemerOmschrijving(dbPersoonWrapper.Overlijden.rni_deelnemer), dbPersoonWrapper.Overlijden.verdrag_oms) });
		}
		// RNI Deelnemer inschrijving may never to delivered. Business logic.
		//if (dbPersoonWrapper.Inschrijving != null
		//	&& dbPersoonWrapper.Inschrijving.rni_deelnemer != null)
		//{
		//	rniDeelnemers.Add("Inschrijving", (dbPersoonWrapper.Inschrijving.rni_deelnemer.Value, dbPersoonWrapper.Inschrijving.pl_rni_deelnemer_omschrijving, dbPersoonWrapper.Inschrijving.verdrag_oms));
		//}
		if (dbPersoonWrapper.Verblijfplaats != null
			&& dbPersoonWrapper.Verblijfplaats.rni_deelnemer != null)
		{
			rniDeelnemerDictionary.Add("Verblijfplaats", new List<(short deelnemerCode, string? deelnemerOmschrijving, string? omschrijvingVerdrag)> { (dbPersoonWrapper.Verblijfplaats.rni_deelnemer.Value, dbPersoonWrapper.Verblijfplaats.verblfpls_rni_deelnemer_omschrijving ?? await _domeinTabellenHelper.GetRniDeelnemerOmschrijving(dbPersoonWrapper.Verblijfplaats.rni_deelnemer), dbPersoonWrapper.Verblijfplaats.verdrag_oms) });
		}

		var rniDeelnemers = rniDeelnemerDictionary
			.Where(deelnemer => !deelnemer.Key.Equals("Nationaliteit"))
			.Select(deelnemer => new RniDeelnemer
			{
				Categorie = deelnemer.Key,
				Deelnemer = new Waardetabel
				{
					Code = deelnemer.Value.FirstOrDefault().deelnemerCode.ToString()?.PadLeft(4, '0'),
					Omschrijving = deelnemer.Value.FirstOrDefault().deelnemerOmschrijving
				},
				OmschrijvingVerdrag = deelnemer.Value.FirstOrDefault().omschrijvingVerdrag
			})
			.ToList();

		if (rniDeelnemerDictionary.ContainsKey("Nationaliteit") && rniDeelnemerDictionary["Nationaliteit"].Any())
		{
			rniDeelnemers.AddRange(rniDeelnemerDictionary["Nationaliteit"]
				.Select(deelnemer => new RniDeelnemer
				{
					Categorie = "Nationaliteit",
					Deelnemer = new Waardetabel
					{
						Code = deelnemer.deelnemerCode.ToString()?.PadLeft(4, '0'),
						Omschrijving = deelnemer.deelnemerOmschrijving
					},
					OmschrijvingVerdrag = deelnemer.omschrijvingVerdrag
				}));
		}

		return rniDeelnemers;
	}
}