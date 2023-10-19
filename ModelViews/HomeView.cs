namespace api.ModelViews;

public record HomeView
{
   //public string Informacao {get {return "Bem vindo ao sistema";}}
   public string Informacao => "Bem-vindo ao sistema";
   public List<dynamic> Endpoints => new List<dynamic>(){ 
    new { Item= new {
        Documentacao = "/swagger"
    }},
    new { Item= new {
        Path = "/alunos"
    }}    
    };
}
