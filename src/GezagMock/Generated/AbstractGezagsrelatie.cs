namespace Brp.Gezag.Mock.Generated;

[Newtonsoft.Json.JsonConverter(typeof(JsonInheritanceConverter), "type")]
[JsonInheritanceAttribute("TweehoofdigOuderlijkGezag", typeof(TweehoofdigOuderlijkGezag))]
[JsonInheritanceAttribute("EenhoofdigOuderlijkGezag", typeof(EenhoofdigOuderlijkGezag))]
[JsonInheritanceAttribute("GezamenlijkGezag", typeof(GezamenlijkGezag))]
[JsonInheritanceAttribute("Voogdij", typeof(Voogdij))]
[JsonInheritanceAttribute("GezagNietTeBepalen", typeof(GezagNietTeBepalen))]
[JsonInheritanceAttribute("TijdelijkGeenGezag", typeof(TijdelijkGeenGezag))]
[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
public partial class AbstractGezagsrelatie
{

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties;

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties ?? (_additionalProperties = new System.Collections.Generic.Dictionary<string, object>()); }
        set { _additionalProperties = value; }
    }

    [Newtonsoft.Json.JsonProperty("inOnderzoek", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public Boolean? InOnderzoek { get; set; }
}

public partial class TijdelijkGeenGezag : AbstractGezagsrelatie
{
}
