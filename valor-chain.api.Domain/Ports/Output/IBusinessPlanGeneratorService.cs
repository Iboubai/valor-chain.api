using valor_chain.api.Domain.Entities;

namespace valor_chain.api.Domain.Ports.Output;

public interface IBusinessPlanGeneratorService
{
    Task<string> GenerateBusinessPlanPdfAsync(Project project);
}