
using System.Reflection;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
    public partial class PersonenQueryResponse
    {
        public static PersonenQueryResponse? MapFrom(Deprecated.PersonenQueryResponse response)
        {
            if (response == null) return null;

            var sourceType = response.GetType();
            var targetTypeName = sourceType.Name.Replace("Deprecated.", string.Empty);
            var targetType = typeof(PersonenQueryResponse).Assembly.GetTypes()
                .FirstOrDefault(t => t.Name == targetTypeName);

            if (targetType == null)
                throw new InvalidOperationException($"No matching target type found for {sourceType.Name}");

            var result = Activator.CreateInstance(targetType);

            var sourceProps = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProps = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProp in sourceProps)
            {
                var targetProp = targetProps.FirstOrDefault(p => p.Name == sourceProp.Name && p.CanWrite);
                if (targetProp == null)
                    continue;

                if (sourceProp.Name == "Personen" && sourceProp.PropertyType.IsGenericType)
                {
                    var sourceList = sourceProp.GetValue(response) as IEnumerable;
                    if (sourceList != null)
                    {
                        var targetListType = targetProp.PropertyType.GetGenericArguments().First();
                        var targetList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(targetListType));

                        foreach (var sourcePerson in sourceList)
                        {
                            var mappedPerson = MapGbaPersoonWithoutGezag(sourcePerson, targetListType);
                            if (mappedPerson != null)
                                targetList.Add(mappedPerson);
                        }
                        targetProp.SetValue(result, targetList);
                    }
                }
                else
                {
                    var value = sourceProp.GetValue(response);
                    targetProp.SetValue(result, value);
                }
            }

            return (PersonenQueryResponse)result;
        }

        private static object? MapGbaPersoonWithoutGezag(object sourcePerson, Type targetType)
        {
            if (sourcePerson == null) return null;

            var result = Activator.CreateInstance(targetType);
            var sourceProps = sourcePerson.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProps = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProp in sourceProps)
            {
                var targetProp = targetProps.FirstOrDefault(p => p.Name == sourceProp.Name && p.CanWrite);

                if (targetProp != null)
                {
                    if (string.Equals(sourceProp.Name, "gezag", StringComparison.OrdinalIgnoreCase))
                    {
                        targetProp.SetValue(result, new List<Gezagsrelatie>());
                        continue;
                    }
                    var value = sourceProp.GetValue(sourcePerson);
                    targetProp.SetValue(result, value);
                }
            }
            return result;
        }
    }
}