namespace PromFuture.TestTask.Models
{
    public class TerminalResponse
    {
        public int terminal_id { get; set; }
        public string user_name { get; set; }
        public int command_id { get; set; }
        public int parameter1 { get; set; }
        public int parameter2 { get; set; }
        public int parameter3 { get; set; }
        public int parameter4 { get; set; }
        public int parameter5 { get; set; }
        public int parameter6 { get; set; }
        public int parameter7 { get; set; }
        public int parameter8 { get; set; }
        public string str_parameter1 { get; set; }
        public string str_parameter2 { get; set; }
        public int state { get; set; }
        public string state_name { get; set; }
        public DateTime? time_created { get; set; }
        public DateTime? time_delivered { get; set; }
        public int id { get; set; }
    }
}
