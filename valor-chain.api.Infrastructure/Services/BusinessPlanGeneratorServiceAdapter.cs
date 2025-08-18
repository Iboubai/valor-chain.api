using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Output;

namespace valor_chain.api.Infrastructure.Services
{
    public class BusinessPlanGeneratorServiceAdapter : IBusinessPlanGeneratorService
    {
        public async Task<string> GenerateBusinessPlanPdfAsync(Project project)
        {
            // Simulate generating a PDF business plan
            // In reality, this could involve calling another microservice
            // or complex document generation logic.
            Console.WriteLine($"Generating business plan for project: { project.Name}");
            await Task.Delay(1000); // Simulate processing delay
            return $"path/to/business_plan_{project.Id}.pdf";
        }
    }

}
