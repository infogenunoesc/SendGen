﻿namespace SendGen.Domain.OpaSuiteDomains
{
    public class Contato
    {
        public string canalCliente { get; set; }
    }

    public class TemplateSend
    {
        public Contato contato { get; set; }
        public Template template { get; set; }
        public string canal { get; set; }
    }

    public class Template
    {
        public string _id { get; set; }
        public List<string> variaveis { get; set; }
    }


}
