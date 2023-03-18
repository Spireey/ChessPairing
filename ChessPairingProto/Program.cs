using ChessPairingProto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

PairingManager pairingManager = new PairingManager(7);
pairingManager.AddPlayer(new PairingManager.Player("Valentin", 1199));
pairingManager.AddPlayer(new PairingManager.Player("Jules", 1199));
pairingManager.AddPlayer(new PairingManager.Player("MVL", 1199));
pairingManager.AddPlayer(new PairingManager.Player("Hikaru", 1199));
pairingManager.AddPlayer(new PairingManager.Player("Magnus", 1199));
pairingManager.AddPlayer(new PairingManager.Player("Balcer", 1199));
pairingManager.AddPlayer(new PairingManager.Player("Baki", 1199));
pairingManager.AddPlayer(new PairingManager.Player("Paulo", 1199));
pairingManager.AddPlayer(new PairingManager.Player("Flavien", 1199));
pairingManager.AddPlayer(new PairingManager.Player("Caire", 1199));

pairingManager.CreateNewMatches();
pairingManager.ShowCurrentMatches();
pairingManager.EnterScore(1, "1-0");
pairingManager.EnterScore(1, "1-0");
pairingManager.EnterScore(3, "0-1");
pairingManager.EnterScore(1, "1-0");
pairingManager.EnterScore(1, "1/2-1/2");

pairingManager.ShowScoreBoard();

pairingManager.CreateNewMatches();
pairingManager.ShowCurrentMatches();
pairingManager.EnterScore(2, "1-0");
pairingManager.EnterScore(4, "1-0");
pairingManager.EnterScore(3, "0-1");
pairingManager.EnterScore(2, "1-0");
pairingManager.EnterScore(1, "1/2-1/2");

pairingManager.ShowScoreBoard();

pairingManager.CreateNewMatches();
pairingManager.ShowCurrentMatches();
pairingManager.EnterScore(5, "1-0");
pairingManager.EnterScore(2, "1-0");
pairingManager.EnterScore(1, "0-1");
pairingManager.EnterScore(1, "1-0");
pairingManager.EnterScore(1, "1/2-1/2");

pairingManager.ShowScoreBoard();
pairingManager.ShowAllPlayerHistory();
//app.Run();