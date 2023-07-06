namespace RazorMovieTutorial.DataTables
{
    public class DataTablesRequest
    {
        public int draw { get; set; }
        public IEnumerable<Column>? columns { get; set; }

        public IEnumerable<Order>? order { get; set; }

        public int start { get; set; }

        public int length { get; set; }

        public Search? search { get; set; }

    }
}
