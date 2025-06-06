using System.Reflection;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;

public partial class PersonenQuery
{
    public static PersonenQuery MapFrom(BRP.PersonenQuery model)
    {
        if (model == null) return null;

        var result = new PersonenQuery();

        var sourceProps = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var targetProps = typeof(PersonenQuery).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var sourceProp in sourceProps)
        {
            var targetProp = targetProps.FirstOrDefault(p => p.Name == sourceProp.Name && p.PropertyType == sourceProp.PropertyType && p.CanWrite);
            if (targetProp != null)
            {
                var value = sourceProp.GetValue(model);
                targetProp.SetValue(result, value);
            }
        }

        return result;
    }
}