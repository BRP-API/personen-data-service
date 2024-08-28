using Rvig.Api.Gezag.ResponseModels;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Shared.ApiModels.PersonenHistorieBase;

namespace Rvig.HaalCentraalApi.Personen.Interfaces;

public interface IGezagTransformer
{
	/// <summary>
	/// Transform Gezagsmodule Gezagsrelatie response to Gezag compatible with BRP personen API relation.
	/// </summary>
	/// <param name="gezagsRelaties"></param>
	/// <returns></returns>
	IEnumerable<AbstractGezagsrelatie> TransformGezagsrelaties(IEnumerable<Gezagsrelatie> gezagsRelaties, string? persoonBurgerservicenummer, List<GbaPartner>? persoonPartners, List<GbaKind>? persoonKinderen, List<GbaOuder>? persoonOuders);
}
