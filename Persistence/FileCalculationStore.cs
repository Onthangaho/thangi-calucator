using System.Text.Json;

namespace thangi_calucator.Persistence
{
 public class FileCalculationStore : ICalculationStore
    {
        private readonly string _filePath;

        public FileCalculationStore(string filePath)
        {
            _filePath = filePath;
        }

        public async Task SaveAsync(Calculation calculation)
        {
            var calculations = await LoadAllAsync();
            var updatedCalculations = calculations.ToList();
            updatedCalculations.Add(calculation);

            var json = JsonSerializer.Serialize(updatedCalculations);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task<IReadOnlyList<Calculation>> LoadAllAsync()
        {
            // If the file does not exist, return an empty list
            if (!File.Exists(_filePath))
            {
                return new List<Calculation>();
            }
     
            // Read the file and deserialize the calculations 
            var json = await File.ReadAllTextAsync(_filePath);
            // Handle empty file case
            return JsonSerializer.Deserialize<List<Calculation>>(json) ?? new List<Calculation>();
        }
    }
}