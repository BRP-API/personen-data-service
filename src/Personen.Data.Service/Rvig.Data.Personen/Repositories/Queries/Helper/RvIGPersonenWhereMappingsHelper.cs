using Rvig.Data.Base.Postgres.DatabaseModels;
using Rvig.Data.Base.Postgres.Repositories.Queries;

namespace Rvig.Data.Personen.Repositories.Queries.Helper;

public class RvIGPersonenWhereMappingsHelper : RvIGBaseWhereMappingsHelper
{
	public static IDictionary<string, string> GetPersoonMappings() => GetPersoonBaseMappings().Concat(new Dictionary<string, string>()
	{
		// lo3_pl / DbPersoonActueelWrapper.Inschrijving
		["pl.pl_id as pl_pl_id"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.pl_id)}",
		["pl.pl_blokkering_start_datum as pl_pl_blokkering_start_datum"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.pl_blokkering_start_datum)}",
		["pl.bijhouding_opschort_datum as pl_bijhouding_opschort_datum"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.bijhouding_opschort_datum)}",
		["pl.bijhouding_opschort_reden as pl_bijhouding_opschort_reden"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.bijhouding_opschort_reden)}",
		["pl.gba_eerste_inschrijving_datum as pl_gba_eerste_inschrijving_datum"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.gba_eerste_inschrijving_datum)}",
		["pl.geheim_ind as pl_geheim_ind"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.geheim_ind)}",
		["pl.europees_kiesrecht_aand as pl_europees_kiesrecht_aand"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.europees_kiesrecht_aand)}",
		["pl.europees_kiesrecht_datum as pl_europees_kiesrecht_datum"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.europees_kiesrecht_datum)}",
		["pl.europees_uitsluit_eind_datum as pl_europees_uitsluit_eind_datum"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.europees_uitsluit_eind_datum)}",
		["pl.kiesrecht_uitgesl_aand as pl_kiesrecht_uitgesl_aand"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.kiesrecht_uitgesl_aand)}",
		["pl.kiesrecht_uitgesl_eind_datum as pl_kiesrecht_uitgesl_eind_datum"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.kiesrecht_uitgesl_eind_datum)}",
		["pl.kiesrecht_doc_gemeente_code as pl_kiesrecht_doc_gemeente_code"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.kiesrecht_doc_gemeente_code)}",
		["pl.kiesrecht_doc_datum as pl_kiesrecht_doc_datum"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.kiesrecht_doc_datum)}",
		["pl.kiesrecht_doc_beschrijving as pl_kiesrecht_doc_beschrijving"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.kiesrecht_doc_beschrijving)}",
		["pl.verificatie_datum as pl_verificatie_datum"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.verificatie_datum)}",
		["pl.verificatie_oms as pl_verificatie_oms"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.verificatie_oms)}",

		// Business rules logic doesn't want this.
		//["pl.rni_deelnemer as pl_rni_deelnemer"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.rni_deelnemer)}",
		//["pl.verdrag_oms as pl_verdrag_oms"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.verdrag_oms)}",

		// Will no longer be get by main query but via dictionary.
		//["tp.titel_predicaat_oms"] = $"{nameof(DbPersoonActueelWrapper.Persoon)}.{nameof(lo3_pl_persoon.titel_predicaat_oms)}",
		//["tp.titel_predicaat_soort"] = $"{nameof(DbPersoonActueelWrapper.Persoon)}.{nameof(lo3_pl_persoon.titel_predicaat_soort)}",
		//["gebrte_plts.gemeente_naam as geboorte_plaats_naam"] = $"{nameof(DbPersoonActueelWrapper.Persoon)}.{nameof(lo3_pl_persoon.geboorte_plaats_naam)}",
		//["gebrte_land.land_naam as geboorte_land_naam"] = $"{nameof(DbPersoonActueelWrapper.Persoon)}.{nameof(lo3_pl_persoon.geboorte_land_naam)}",

		// Business rules logic doesn't want this.
		//["rni_deelnemer_pl.deelnemer_oms as pl_rni_deelnemer_omschrijving"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.pl_rni_deelnemer_omschrijving)}",

		// lo3_pl_verblijfplaats / DbPersoonActueelWrapper.Verblijfplaats
		["verblfpls.pl_id as verblfpl_pl_id"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.pl_id)}",
		["verblfpls.volg_nr as verblfpl_volg_nr"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.volg_nr)}",
		["verblfpls.inschrijving_gemeente_code as verblfpl_inschrijving_gemeente_code"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.inschrijving_gemeente_code)}",
		["verblfpls.adres_id as verblfpl_adres_id"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.adres_id)}",
		["verblfpls.inschrijving_datum as verblfpl_inschrijving_datum"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.inschrijving_datum)}",
		["verblfpls.adres_functie as verblfpl_adres_functie"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.adres_functie)}",
		["verblfpls.gemeente_deel as verblfpl_gemeente_deel"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.gemeente_deel)}",
		["verblfpls.adreshouding_start_datum as verblfpl_adreshouding_start_datum"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.adreshouding_start_datum)}",
		["verblfpls.vertrek_land_code as verblfpl_vertrek_land_code"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.vertrek_land_code)}",
		["verblfpls.vertrek_datum as verblfpl_vertrek_datum"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.vertrek_datum)}",
		["verblfpls.vertrek_land_adres_1 as verblfpl_vertrek_land_adres_1"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.vertrek_land_adres_1)}",
		["verblfpls.vertrek_land_adres_2 as verblfpl_vertrek_land_adres_2"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.vertrek_land_adres_2)}",
		["verblfpls.vertrek_land_adres_3 as verblfpl_vertrek_land_adres_3"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.vertrek_land_adres_3)}",
		["verblfpls.vestiging_land_code as verblfpl_vestiging_land_code"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.vestiging_land_code)}",
		["verblfpls.vestiging_datum as verblfpl_vestiging_datum"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.vestiging_datum)}",
		["verblfpls.aangifte_adreshouding_oms as verblfpl_aangifte_adreshouding_oms"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.aangifte_adreshouding_oms)}",
		["verblfpls.onderzoek_gegevens_aand as verblfpl_onderzoek_gegevens_aand"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.onderzoek_gegevens_aand)}",
		["verblfpls.onderzoek_start_datum as verblfpl_onderzoek_start_datum"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.onderzoek_start_datum)}",
		["verblfpls.onderzoek_eind_datum as verblfpl_onderzoek_eind_datum"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.onderzoek_eind_datum)}",
		["verblfpls.onjuist_ind as verblfpl_onjuist_ind"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.onjuist_ind)}",
		["verblfpls.geldigheid_start_datum as verblfpl_geldigheid_start_datum"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.geldigheid_start_datum)}",
		["verblfpls.opneming_datum as verblfpl_opneming_datum"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.opneming_datum)}",
		["verblfpls.rni_deelnemer as verblfpl_rni_deelnemer"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.rni_deelnemer)}",
		["verblfpls.verdrag_oms as verblfpl_verdrag_oms"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.verdrag_oms)}",

		// Will no longer be get by main query but via dictionary.
		//["inschrvng_plts.gemeente_naam as verblfpl_inschrijving_gemeente_naam"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.inschrijving_gemeente_naam)}",
		//["vestgng_land.land_naam as verblfpl_vestiging_land_naam"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.vestiging_land_naam)}",
		//["vertrk_land.land_naam as verblfpl_vertrek_land_naam"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.vertrek_land_naam)}",
		//["rni_deelnemer_verblfpls.deelnemer_oms as verblfpls_rni_deelnemer_omschrijving"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.verblfpls_rni_deelnemer_omschrijving)}",

		// lo3_adres / DbPersoonActueelWrapper.Adres
		["adres.adres_id as adres_adres_id"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.adres_id)}",
		["adres.gemeente_code as adres_gemeente_code"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.gemeente_code)}",
		["adres.straat_naam as adres_straat_naam"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.straat_naam)}",
		["adres.diak_straat_naam as adres_diak_straat_naam"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.diak_straat_naam)}",
		["adres.huis_nr as adres_huis_nr"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.huis_nr)}",
		["adres.huis_letter as adres_huis_letter"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.huis_letter)}",
		["adres.huis_nr_toevoeging as adres_huis_nr_toevoeging"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.huis_nr_toevoeging)}",
		["adres.huis_nr_aand as adres_huis_nr_aand"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.huis_nr_aand)}",
		["adres.postcode as adres_postcode"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.postcode)}",
		["adres.locatie_beschrijving as adres_locatie_beschrijving"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.locatie_beschrijving)}",
		["adres.diak_locatie_beschrijving as adres_diak_locatie_beschrijving"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.diak_locatie_beschrijving)}",
		["adres.open_ruimte_naam as adres_open_ruimte_naam"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.open_ruimte_naam)}",
		["adres.diak_open_ruimte_naam as adres_diak_open_ruimte_naam"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.diak_open_ruimte_naam)}",
		["adres.woon_plaats_naam as adres_woon_plaats_naam"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.woon_plaats_naam)}",
		["adres.diak_woon_plaats_naam as adres_diak_woon_plaats_naam"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.diak_woon_plaats_naam)}",
		["adres.verblijf_plaats_ident_code as adres_verblijf_plaats_ident_code"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.verblijf_plaats_ident_code)}",
		["adres.nummer_aand_ident_code as adres_nummer_aand_ident_code"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.nummer_aand_ident_code)}",

		// lo3_pl_overlijden / DbPersoonActueelWrapper.Overlijden
		["overlijden.pl_id as overlijden_pl_id"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.pl_id)}",
		["overlijden.volg_nr as overlijden_volg_nr"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.volg_nr)}",
		["overlijden.overlijden_datum as overlijden_overlijden_datum"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.overlijden_datum)}",
		["overlijden.overlijden_plaats as overlijden_overlijden_plaats"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.overlijden_plaats)}",
		["overlijden.overlijden_land_code as overlijden_overlijden_land_code"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.overlijden_land_code)}",
		["overlijden.onderzoek_gegevens_aand as overlijden_onderzoek_gegevens_aand"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.onderzoek_gegevens_aand)}",
		["overlijden.onderzoek_start_datum as overlijden_onderzoek_start_datum"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.onderzoek_start_datum)}",
		["overlijden.onderzoek_eind_datum as overlijden_onderzoek_eind_datum"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.onderzoek_eind_datum)}",
		["overlijden.onjuist_ind as overlijden_onjuist_ind"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.onjuist_ind)}",
		["overlijden.rni_deelnemer as overlijden_rni_deelnemer"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.rni_deelnemer)}",
		["overlijden.verdrag_oms as overlijden_verdrag_oms"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.verdrag_oms)}",


		// Will no longer be get by main query but via dictionary.
		//["overlijden_plts.gemeente_naam as overlijden_overlijden_plaats_naam"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.overlijden_plaats_naam)}",
		//["overlijden_land.land_naam as overlijden_overlijden_land_naam"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.overlijden_land_naam)}",
		//["rni_deelnemer_overlijden.deelnemer_oms as overlijden_rni_deelnemer_omschrijving"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.overlijden_rni_deelnemer_omschrijving)}",

		// lo3_pl_gezagsverhouding / DbPersoonActueelWrapper.Gezagsverhouding
		["gezgvhdng.pl_id as gezgvhdng_pl_id"] = $"{nameof(DbPersoonActueelWrapper.Gezagsverhouding)}.{nameof(lo3_pl_gezagsverhouding.pl_id)}",
		["gezgvhdng.volg_nr as gezgvhdng_volg_nr"] = $"{nameof(DbPersoonActueelWrapper.Gezagsverhouding)}.{nameof(lo3_pl_gezagsverhouding.volg_nr)}",
		["gezgvhdng.minderjarig_gezag_ind as gezgvhdng_minderjarig_gezag_ind"] = $"{nameof(DbPersoonActueelWrapper.Gezagsverhouding)}.{nameof(lo3_pl_gezagsverhouding.minderjarig_gezag_ind)}",

		// Will no longer be get by main query but via dictionary.
		//["mind_gezag_waarde_tabel.gezagsverhouding_oms as minderjarig_gezag_oms"] = $"{nameof(DbPersoonActueelWrapper.Gezagsverhouding)}.{nameof(lo3_pl_gezagsverhouding.minderjarig_gezag_oms)}",

		["gezgvhdng.curatele_register_ind as gezgvhdng_curatele_register_ind"] = $"{nameof(DbPersoonActueelWrapper.Gezagsverhouding)}.{nameof(lo3_pl_gezagsverhouding.curatele_register_ind)}",
		["gezgvhdng.onderzoek_gegevens_aand as gezgvhdng_onderzoek_gegevens_aand"] = $"{nameof(DbPersoonActueelWrapper.Gezagsverhouding)}.{nameof(lo3_pl_gezagsverhouding.onderzoek_gegevens_aand)}",
		["gezgvhdng.onderzoek_start_datum as gezgvhdng_onderzoek_start_datum"] = $"{nameof(DbPersoonActueelWrapper.Gezagsverhouding)}.{nameof(lo3_pl_gezagsverhouding.onderzoek_start_datum)}",
		["gezgvhdng.onderzoek_eind_datum as gezgvhdng_onderzoek_eind_datum"] = $"{nameof(DbPersoonActueelWrapper.Gezagsverhouding)}.{nameof(lo3_pl_gezagsverhouding.onderzoek_eind_datum)}",
		["gezgvhdng.onjuist_ind as gezgvhdng_onjuist_ind"] = $"{nameof(DbPersoonActueelWrapper.Gezagsverhouding)}.{nameof(lo3_pl_gezagsverhouding.onjuist_ind)}",

		// lo3_pl_verblijfstitel / DbPersoonActueelWrapper.Verblijfstitel
		["verblftl.pl_id as verblftl_pl_id"] = $"{nameof(DbPersoonActueelWrapper.Verblijfstitel)}.{nameof(lo3_pl_verblijfstitel.pl_id)}",
		["verblftl.volg_nr as verblftl_volg_nr"] = $"{nameof(DbPersoonActueelWrapper.Verblijfstitel)}.{nameof(lo3_pl_verblijfstitel.volg_nr)}",
		["verblftl.verblijfstitel_aand as verblftl_verblijfstitel_aand"] = $"{nameof(DbPersoonActueelWrapper.Verblijfstitel)}.{nameof(lo3_pl_verblijfstitel.verblijfstitel_aand)}",
		["verblftl.verblijfstitel_eind_datum as verblftl_verblijfstitel_eind_datum"] = $"{nameof(DbPersoonActueelWrapper.Verblijfstitel)}.{nameof(lo3_pl_verblijfstitel.verblijfstitel_eind_datum)}",
		["verblftl.verblijfstitel_start_datum as verblftl_verblijfstitel_start_datum"] = $"{nameof(DbPersoonActueelWrapper.Verblijfstitel)}.{nameof(lo3_pl_verblijfstitel.verblijfstitel_start_datum)}",
		["verblftl.onderzoek_gegevens_aand as verblftl_onderzoek_gegevens_aand"] = $"{nameof(DbPersoonActueelWrapper.Verblijfstitel)}.{nameof(lo3_pl_verblijfstitel.onderzoek_gegevens_aand)}",
		["verblftl.onderzoek_start_datum as verblftl_onderzoek_start_datum"] = $"{nameof(DbPersoonActueelWrapper.Verblijfstitel)}.{nameof(lo3_pl_verblijfstitel.onderzoek_start_datum)}",
		["verblftl.onderzoek_eind_datum as verblftl_onderzoek_eind_datum"] = $"{nameof(DbPersoonActueelWrapper.Verblijfstitel)}.{nameof(lo3_pl_verblijfstitel.onderzoek_eind_datum)}",
		["verblftl.onjuist_ind as verblftl_onjuist_ind"] = $"{nameof(DbPersoonActueelWrapper.Verblijfstitel)}.{nameof(lo3_pl_verblijfstitel.onjuist_ind)}",

		// Will no longer be get by main query but via dictionary.
		//["verblftl_oms.verblijfstitel_aand_oms as verblftl_verblijfstitel_aand_oms"] = $"{nameof(DbPersoonActueelWrapper.Verblijfstitel)}.{nameof(lo3_pl_verblijfstitel.verblijfstitel_aand_oms)}"
	}).ToDictionary(x => x.Key, x => x.Value);

	public static IDictionary<string, string> GetPersoonBeperktMappings() => GetPersoonBeperktBaseMappings().Concat(new Dictionary<string, string>()
	{
		// lo3_pl / DbPersoonActueelWrapper.Inschrijving
		["pl.pl_id as pl_pl_id"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.pl_id)}",
		["pl.pl_blokkering_start_datum as pl_pl_blokkering_start_datum"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.pl_blokkering_start_datum)}",
		["pl.bijhouding_opschort_datum as pl_bijhouding_opschort_datum"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.bijhouding_opschort_datum)}",
		["pl.bijhouding_opschort_reden as pl_bijhouding_opschort_reden"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.bijhouding_opschort_reden)}",
		["pl.geheim_ind as pl_geheim_ind"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.geheim_ind)}",
		["pl.verificatie_datum as pl_verificatie_datum"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.verificatie_datum)}",
		["pl.verificatie_oms as pl_verificatie_oms"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.verificatie_oms)}",

		// Business rules logic doesn't want this.
		//["pl.rni_deelnemer as pl_rni_deelnemer"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.rni_deelnemer)}",
		//["pl.verdrag_oms as pl_verdrag_oms"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.verdrag_oms)}",

		// Will no longer be get by main query but via dictionary.
		//["tp.titel_predicaat_oms"] = $"{nameof(DbPersoonActueelWrapper.Persoon)}.{nameof(lo3_pl_persoon.titel_predicaat_oms)}",
		//["tp.titel_predicaat_soort"] = $"{nameof(DbPersoonActueelWrapper.Persoon)}.{nameof(lo3_pl_persoon.titel_predicaat_soort)}",

		// Business rules logic doesn't want this.
		//["rni_deelnemer_pl.deelnemer_oms as pl_rni_deelnemer_omschrijving"] = $"{nameof(DbPersoonActueelWrapper.Inschrijving)}.{nameof(lo3_pl.pl_rni_deelnemer_omschrijving)}",

		// lo3_pl_verblijfplaats / DbPersoonActueelWrapper.Verblijfplaats
		["verblfpls.pl_id as verblfpl_pl_id"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.pl_id)}",
		["verblfpls.volg_nr as verblfpl_volg_nr"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.volg_nr)}",
		["verblfpls.inschrijving_gemeente_code as verblfpl_inschrijving_gemeente_code"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.inschrijving_gemeente_code)}",
		["verblfpls.adres_id as verblfpl_adres_id"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.adres_id)}",
		["verblfpls.vertrek_land_code as verblfpl_vertrek_land_code"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.vertrek_land_code)}",
		["verblfpls.vertrek_land_adres_1 as verblfpl_vertrek_land_adres_1"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.vertrek_land_adres_1)}",
		["verblfpls.vertrek_land_adres_2 as verblfpl_vertrek_land_adres_2"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.vertrek_land_adres_2)}",
		["verblfpls.vertrek_land_adres_3 as verblfpl_vertrek_land_adres_3"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.vertrek_land_adres_3)}",
		["verblfpls.onderzoek_gegevens_aand as verblfpl_onderzoek_gegevens_aand"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.onderzoek_gegevens_aand)}",
		["verblfpls.onderzoek_start_datum as verblfpl_onderzoek_start_datum"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.onderzoek_start_datum)}",
		["verblfpls.onderzoek_eind_datum as verblfpl_onderzoek_eind_datum"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.onderzoek_eind_datum)}",
		["verblfpls.onjuist_ind as verblfpl_onjuist_ind"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.onjuist_ind)}",
		["verblfpls.rni_deelnemer as verblfpl_rni_deelnemer"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.rni_deelnemer)}",
		["verblfpls.verdrag_oms as verblfpl_verdrag_oms"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.verdrag_oms)}",

		// Will no longer be get by main query but via dictionary.
		//["inschrvng_plts.gemeente_naam as verblfpl_inschrijving_gemeente_naam"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.inschrijving_gemeente_naam)}",
		//["vertrk_land.land_naam as verblfpl_vertrek_land_naam"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.vertrek_land_naam)}",
		//["rni_deelnemer_verblfpls.deelnemer_oms as verblfpls_rni_deelnemer_omschrijving"] = $"{nameof(DbPersoonActueelWrapper.Verblijfplaats)}.{nameof(lo3_pl_verblijfplaats.verblfpls_rni_deelnemer_omschrijving)}",

		// lo3_adres / DbPersoonActueelWrapper.Adres
		["adres.adres_id as adres_adres_id"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.adres_id)}",
		["adres.gemeente_code as adres_gemeente_code"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.gemeente_code)}",
		["adres.straat_naam as adres_straat_naam"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.straat_naam)}",
		["adres.diak_straat_naam as adres_diak_straat_naam"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.diak_straat_naam)}",
		["adres.huis_nr as adres_huis_nr"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.huis_nr)}",
		["adres.huis_letter as adres_huis_letter"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.huis_letter)}",
		["adres.huis_nr_toevoeging as adres_huis_nr_toevoeging"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.huis_nr_toevoeging)}",
		["adres.huis_nr_aand as adres_huis_nr_aand"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.huis_nr_aand)}",
		["adres.postcode as adres_postcode"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.postcode)}",
		["adres.locatie_beschrijving as adres_locatie_beschrijving"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.locatie_beschrijving)}",
		["adres.diak_locatie_beschrijving as adres_diak_locatie_beschrijving"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.diak_locatie_beschrijving)}",
		["adres.woon_plaats_naam as adres_woon_plaats_naam"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.woon_plaats_naam)}",
		["adres.diak_woon_plaats_naam as adres_diak_woon_plaats_naam"] = $"{nameof(DbPersoonActueelWrapper.Adres)}.{nameof(lo3_adres.diak_woon_plaats_naam)}",

		// lo3_pl_overlijden / DbPersoonActueelWrapper.Overlijden
		// apparantly not in use anymore
		//["overlijden.pl_id as overlijden_pl_id"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.pl_id)}",
		//["overlijden.volg_nr as overlijden_volg_nr"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.volg_nr)}",
		//["overlijden.overlijden_datum as overlijden_overlijden_datum"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.overlijden_datum)}",
		//["overlijden.onderzoek_gegevens_aand as overlijden_onderzoek_gegevens_aand"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.onderzoek_gegevens_aand)}",
		//["overlijden.onderzoek_start_datum as overlijden_onderzoek_start_datum"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.onderzoek_start_datum)}",
		//["overlijden.onderzoek_eind_datum as overlijden_onderzoek_eind_datum"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.onderzoek_eind_datum)}",
		//["overlijden.onjuist_ind as overlijden_onjuist_ind"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.onjuist_ind)}",
		//["overlijden.rni_deelnemer as overlijden_rni_deelnemer"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.rni_deelnemer)}",
		//["overlijden.verdrag_oms as overlijden_verdrag_oms"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.verdrag_oms)}",

		// Will no longer be get by main query but via dictionary.
		//["rni_deelnemer_overlijden.deelnemer_oms as overlijden_rni_deelnemer_omschrijving"] = $"{nameof(DbPersoonActueelWrapper.Overlijden)}.{nameof(lo3_pl_overlijden.overlijden_rni_deelnemer_omschrijving)}",
	}).ToDictionary(x => x.Key, x => x.Value);
}