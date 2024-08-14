using Proposal.API.Interfaces;
using Proposal.API.Services;
using Proposal.API.Worker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHostedService<FileQueueWorker>();
builder.Services.AddTransient<IProposalService, ProposalService>();
builder.Services.AddScoped<RabbitService>();


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();