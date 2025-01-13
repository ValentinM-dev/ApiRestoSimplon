namespace ApiRestoSimplon.Class
{
    public class ListArticles
    {
        public required int ArticleId { get; set; }
        public required int CommandId { get; set; }
    }

    public class ListArticlesDTO
    {
        public required int ArticleId { get; set; }
        public required int CommandId { get; set; }

        public ListArticlesDTO() { }
        public ListArticlesDTO(ListArticles listArticles)
        {
            ArticleId = listArticles.ArticleId;
            CommandId = listArticles.CommandId;
        }
    }
}