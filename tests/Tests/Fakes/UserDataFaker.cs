using AwesomeGithubStats.Core.Models;
using AwesomeGithubStats.Core.Models.Responses;
using AwesomeGithubStats.Core.Store;
using System.Text.Json;

namespace Tests.Fakes
{

    public class UserDataFaker
    {
        public static ContributionsCollection GetFixedContributionsFromYear()
        {
            return JsonSerializer.Deserialize<DefaultResponse<UserData>>(ResponseContribution, GithubOptions.DefaultJson).Data.User.ContributionsCollection;
        }
        public static User GetFixedUserInformation()
        {
            return JsonSerializer.Deserialize<DefaultResponse<UserData>>(ResponseYears, GithubOptions.DefaultJson).Data.User;
        }

        private const string ResponseYears = @"{
  ""data"": {
    ""user"": {
      ""name"": ""Bruno Brito"",
      ""login"": ""brunobritodev"",
      ""contributionsCollection"": {
        ""contributionYears"": [
          2018
        ]
      },
      ""pullRequests"": {
        ""totalCount"": 188
      },
      ""issues"": {
        ""totalCount"": 45
      },
      ""followers"": {
        ""totalCount"": 375
      }
    }
  }
}";

        private const string ResponseContribution = @"{
  ""data"": {
    ""user"": {
      ""name"": ""Bruno Brito"",
      ""login"": ""brunobritodev"",
      ""pullRequests"": {
        ""totalCount"": 188
      },
      ""contributionsCollection"": {
        ""totalCommitContributions"": 1455,
        ""totalRepositoryContributions"": 10,
        ""restrictedContributionsCount"": 2,
        ""pullRequestContributionsByRepository"": [
          {
            ""contributions"": {
              ""totalCount"": 51
            },
            ""repository"": {
              ""nameWithOwner"": ""Allto-Sistemas/Jz.Nasa"",
              ""stargazerCount"": 0
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 2
            },
            ""repository"": {
              ""nameWithOwner"": ""ziyasal/FireSharp"",
              ""stargazerCount"": 603
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 2
            },
            ""repository"": {
              ""nameWithOwner"": ""EduardoPires/EquinoxProject"",
              ""stargazerCount"": 4712
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 1
            },
            ""repository"": {
              ""nameWithOwner"": ""dicataldisky/DACSistemas.Central-de-Cursos.BackEnd"",
              ""stargazerCount"": 0
            }
          }
        ],
        ""commitContributionsByRepository"": [
          {
            ""contributions"": {
              ""totalCount"": 254
            },
            ""repository"": {
              ""nameWithOwner"": ""Allto-Sistemas/Jz.Nasa"",
              ""stargazerCount"": 0
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 230
            },
            ""repository"": {
              ""nameWithOwner"": ""brunobritodev/unimed-painel-admin-interno"",
              ""stargazerCount"": 0
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 230
            },
            ""repository"": {
              ""nameWithOwner"": ""brunobritodev/JPProject.Core"",
              ""stargazerCount"": 42
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 230
            },
            ""repository"": {
              ""nameWithOwner"": ""brunobritodev/JPProject.IdentityServer4.SSO"",
              ""stargazerCount"": 321
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 230
            },
            ""repository"": {
              ""nameWithOwner"": ""brunobritodev/JPProject.IdentityServer4.AdminUI"",
              ""stargazerCount"": 735
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 121
            },
            ""repository"": {
              ""nameWithOwner"": ""Allto-Sistemas/Allto.SSO"",
              ""stargazerCount"": 0
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 15
            },
            ""repository"": {
              ""nameWithOwner"": ""brunobritodev/NetCore-BlueprintApi"",
              ""stargazerCount"": 0
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 15
            },
            ""repository"": {
              ""nameWithOwner"": ""Allto-Sistemas/Allto.Esqueleto"",
              ""stargazerCount"": 0
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 15
            },
            ""repository"": {
              ""nameWithOwner"": ""brunobritodev/DapperExamples"",
              ""stargazerCount"": 6
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 14
            },
            ""repository"": {
              ""nameWithOwner"": ""brunobritodev/IdentityServer4AngularOIDC"",
              ""stargazerCount"": 15
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 12
            },
            ""repository"": {
              ""nameWithOwner"": ""brunobritodev/estudiogalbero"",
              ""stargazerCount"": 0
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 12
            },
            ""repository"": {
              ""nameWithOwner"": ""Allto-Sistemas/Allto.Cliente"",
              ""stargazerCount"": 0
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 12
            },
            ""repository"": {
              ""nameWithOwner"": ""brunobritodev/PersonalWebSite"",
              ""stargazerCount"": 0
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 8
            },
            ""repository"": {
              ""nameWithOwner"": ""Allto-Sistemas/Allto.Infra.CrossCutting.EmpresaManager"",
              ""stargazerCount"": 0
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 8
            },
            ""repository"": {
              ""nameWithOwner"": ""Allto-Sistemas/Allto.EmpresaApi"",
              ""stargazerCount"": 0
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 7
            },
            ""repository"": {
              ""nameWithOwner"": ""Allto-Sistemas/Allto.RegiaoApi"",
              ""stargazerCount"": 0
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 7
            },
            ""repository"": {
              ""nameWithOwner"": ""Allto-Sistemas/Allto.Prestador.SPA"",
              ""stargazerCount"": 0
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 7
            },
            ""repository"": {
              ""nameWithOwner"": ""brunobritodev/Blazor-FirstStep"",
              ""stargazerCount"": 2
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 6
            },
            ""repository"": {
              ""nameWithOwner"": ""brunobritodev/CognitiveServicesExamples"",
              ""stargazerCount"": 2
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 6
            },
            ""repository"": {
              ""nameWithOwner"": ""brunobritodev/BlazorInteropJS"",
              ""stargazerCount"": 0
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 4
            },
            ""repository"": {
              ""nameWithOwner"": ""brunobritodev/AspNetCore-IdentityServer4-Example"",
              ""stargazerCount"": 13
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 4
            },
            ""repository"": {
              ""nameWithOwner"": ""brunobritodev/CapturaCia"",
              ""stargazerCount"": 0
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 2
            },
            ""repository"": {
              ""nameWithOwner"": ""dicataldisky/DACSistemas.Central-de-Cursos.BackEnd"",
              ""stargazerCount"": 0
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 2
            },
            ""repository"": {
              ""nameWithOwner"": ""ziyasal/FireSharp"",
              ""stargazerCount"": 603
            }
          },
          {
            ""contributions"": {
              ""totalCount"": 1
            },
            ""repository"": {
              ""nameWithOwner"": ""Allto-Sistemas/Jz.PedagioSemParar"",
              ""stargazerCount"": 0
            }
          }
        ]
      }
    }
  }
}";
    }
}
