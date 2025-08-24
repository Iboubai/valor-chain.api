namespace valor_chain.api.Domain.Ports.Output.Mapper
{
    public interface IMapper<T, U>
    {
        U ToDto(T entity);
        T ToEntity(U dto);
    }
}
