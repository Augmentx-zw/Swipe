namespace ArkPortal.Domain.QueryHandlers
{
    public interface IQuery<TResult>
    {
    }
    public interface IQueryHandler<TQuery, TResult>
    {
        TResult Handle(TQuery query);
    }
}
