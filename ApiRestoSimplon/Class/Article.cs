namespace ApiRestoSimplon.Class
{
    public class Article
    {
        public int Id { get; set; }
        public required string NameArticle { get; set; }
        public required int CategorieId { get; set; }
        public required double PrixArticle { get; set; }
        public required bool Status { get; set; }
        public List<Command> Commands { get; } = [];
    }

    public class ArticleDTO
    {
        public int Id { get; set; }
        public required string NameArticle { get; set; }
        public required int CategorieId { get; set; }
        public required double PrixArticle { get; set; }
        public required bool Status { get; set; }
        public List<Command> Commands { get; } = [];

        public ArticleDTO() { }

        public ArticleDTO(Article article)
        {
            Id = article.Id;
            NameArticle = article.NameArticle;
            CategorieId = article.CategorieId;
            PrixArticle = article.PrixArticle;
            Status = article.Status;
            Commands = article.Commands;
        }
    }
}
