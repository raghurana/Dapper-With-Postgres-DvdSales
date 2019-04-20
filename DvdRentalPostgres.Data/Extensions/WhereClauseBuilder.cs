using Dapper;

namespace DvdRentalPostgres.Data.Extensions
{
    public class WhereClauseBuilder
    {
        private const string WhereClauseTemplate = "/**where**/";

        private SqlBuilder sqlBuilder;
        private SqlBuilder.Template sqlTemplate;
        private readonly CriteriaJoinStrategy joinStrategy;

        public WhereClauseBuilder(string originalQuery, CriteriaJoinStrategy joinStrategy = CriteriaJoinStrategy.And)
        {
            sqlBuilder        = new SqlBuilder();
            sqlTemplate       = sqlBuilder.AddTemplate($"{originalQuery} {WhereClauseTemplate}");
            this.joinStrategy = joinStrategy;
        }

        public void AddClause(string sql, dynamic parameters)
        {
            if (joinStrategy == CriteriaJoinStrategy.And)
                sqlBuilder.Where(sql, parameters);

            else if (joinStrategy == CriteriaJoinStrategy.Or)
                sqlBuilder.OrWhere(sql, parameters);
        }

        public (string BuiltQuery, object BuiltParams) Build()
        {
            var retVal = (sqlTemplate.RawSql, sqlTemplate.Parameters);
            sqlBuilder = null;
            sqlTemplate = null;
            return retVal;
        }
    }
}