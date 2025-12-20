<p align="center">
  <img src="https://raw.githubusercontent.com/brunobritodev/awesome-github-stats/master/docs/banner.png"/>
</p>
<p align="center">
  Get an accurate data from your contributions and transform into a card to show at your GitHub profile README
  <br>
  <a href="https://awesome-github-stats.azurewebsites.net/" target="_blank">View Demo</a>
<hr>  
</p>
<p align="center">
  <a href="https://awesome-github-stats.azurewebsites.net" target="_blank">    
    <img width="400px" alt="Bruno Brito's GitHub Stats" src="https://awesome-github-stats.azurewebsites.net/user-stats/brunobritodev?theme=tokyonight" />  
  </a>
  <a href="https://awesome-github-stats.azurewebsites.net" target="_blank">    
    <img width="400px" alt="Bruno Brito's GitHub Stats" src="https://awesome-github-stats.azurewebsites.net/user-stats/ralmsdeveloper?theme=github&cardType=octocat" />
  </a>
  <a href="https://awesome-github-stats.azurewebsites.net" target="_blank">    
    <img width="400px" alt="Bruno Brito's GitHub Stats" src="https://awesome-github-stats.azurewebsites.net/user-stats/eduardopires?theme=dracula&cardType=level-alternate" />
  </a>
  <a href="https://awesome-github-stats.azurewebsites.net" target="_blank">    
    <img width="400px" alt="Bruno Brito's GitHub Stats" src="https://awesome-github-stats.azurewebsites.net/user-stats/carloscds?theme=github-dark&cardType=github" />
  </a>
</p>


# Content

- [Content](#content)
  - [🚀 Quick setup](#-quick-setup)
  - [💻 Demo Site](#-demo-site)
  - [💁🏻 SVGs wanted.](#-svgs-wanted)
  - [🖌️ Card Types](#️-card-types)
  - [🎨 Themes](#-themes)
  - [🔧 Options](#-options)
    - [Example](#example)
  - [ℹ️ How these stats are calculated](#ℹ️-how-these-stats-are-calculated)
  - [🤗 Contributing](#-contributing)
  - [🤔 Motivations](#-motivations)
  - [🧰 Installing Requirements](#-installing-requirements)
      - [Requirements](#requirements)
    - [Authorization](#authorization)
    - [Running the tests](#running-the-tests)
  - [💕 Special thanks](#-special-thanks)
  - [🙋‍♂️ Support](#️-support)

## 🚀 Quick setup

1. Copy-paste the markdown below into your GitHub profile README
2. Replace the value after `brunobritodev` with your GitHub username

```md
[![My Awesome Stats](https://awesome-github-stats.azurewebsites.net/user-stats/brunobritodev)](https://git.io/awesome-stats-card)
```

## 💻 Demo Site

Here you can customize your Card with a live preview: <https://awesome-github-stats.azurewebsites.net/>

[![Demo Site](https://raw.githubusercontent.com/brunobritodev/awesome-github-stats/master/docs/awesomestats.gif "Demo Site")](https://awesome-github-stats.azurewebsites.net/)

## 💁🏻 SVGs wanted.

If you are a Jedi Master building SVG, help-me adding new SVG's templates. Then we try to add some data on it!

----

## 🖌️ Card Types

There are few Card types to show your stats. Change `cardType` parameter.

|       Card        |                                                      Preview                                                       |
| :---------------: | :----------------------------------------------------------------------------------------------------------------: |
| `level (default)` |                  ![level](https://awesome-github-stats.azurewebsites.net/user-stats/carloscds)                  |
| `level-alternate` | ![level-alternate](https://awesome-github-stats.azurewebsites.net/user-stats/brunobritodev?cardType=level-alternate) |
|     `github`      |         ![github](https://awesome-github-stats.azurewebsites.net/user-stats/DenverCoder1?cardType=github)          |
|     `octocat`     |        ![octocat](https://awesome-github-stats.azurewebsites.net/user-stats/eduardopires?cardType=octocat)         |

## 🎨 Themes

To enable a theme, append `&theme=` followed by the theme name to the end of the source url:

```md
[![GitHub Streak](https://awesome-github-stats.azurewebsites.net/user-stats/brunobritodev&theme=dark)](https://git.io/awesome-stats-card)
```

|    Theme     |                                                 Preview                                                 |
| :----------: | :-----------------------------------------------------------------------------------------------------: |
|  `default`   |          ![default](https://awesome-github-stats.azurewebsites.net/user-stats/ralmsdeveloper)           |
|    `dark`    |       ![dark](https://awesome-github-stats.azurewebsites.net/user-stats/eduardopires?theme=dark)        |
| `tokyonight` | ![highcontrast](https://awesome-github-stats.azurewebsites.net/user-stats/brunobritodev?theme=tokyonight) |
| More themes! |                   **🎨 [See a list of all available themes](./docs/themes/README.md)**                   |

> If you have come up with a new theme you'd like to share with others, open an issue to add it!

## 🔧 Options

The `user` field is part of url `user-stats/<user>`, which is required. All other fields are optional.

If the `theme` parameter is specified, any color customizations specified will be applied on top of the theme, overriding the theme's values.

|  Parameter   |                       Details                        |                        Example                        |
| :----------: | :--------------------------------------------------: | :---------------------------------------------------: |
|   `theme`    |       The theme to apply (Default: `default`)        | `dark`, `radical`, etc. [🎨➜](./docs/themes/README.md) |
| `show_icons` | Dont shown icons at left of labels (Default: `true`) |                   `true` or `false`                   |
| `preferLogin`|  Use github login instead of name (Default: `false`) |                   `true` or `false`                   |
| `background` |                   Background color                   |      **hex code** (without `#`) or **css color**      |
|   `border`   |                     Border color                     |      **hex code** (without `#`) or **css color**      |
|    `text`    |                  Color of the text                   |      **hex code** (without `#`) or **css color**      |
|   `title`    |              Color of the title at top               |      **hex code** (without `#`) or **css color**      |
|    `icon`    |                  Color of the icons                  |      **hex code** (without `#`) or **css color**      |
|    `ring`    |          Color of the ring around the level          |      **hex code** (without `#`) or **css color**      |


### Example

```md
[![My Awesome Custom Stats](https://awesome-github-stats.azurewebsites.net/user-stats/brunobritodev?theme=tokyonight&Ring=DD2727&Border=13DD57&Text=DD2727)](https://git.io/awesome-stats-card)
```

## ℹ️ How these stats are calculated

This tool uses GitHub GraphQL to compute yours contributions since your first day at GitHub.

To include contributions in private repositories, turn on the setting for "Private contributions" from the dropdown menu above the contribution graph on your profile page.

Contributions include:

* Public commits
* Private commits
* Commits in your repositories
* Commits made in another repositories
* Pull requests to your repositories 
* Pull requests to another repositories
* Issues
* Direct stars - Stars from your repositories
* Indirect Stars - Stars from repo's you have made some contribution
* Repositories you have created 

If stats not showing look at [Learn more about what is considered a contribution](https://docs.github.com/articles/why-are-my-contributions-not-showing-up-on-my-profile).

> Note: You may need to wait up to 24 hours for new contributions to show up ([Learn how contributions are counted](https://docs.github.com/articles/why-are-my-contributions-not-showing-up-on-my-profile))

> Note: To prevent too many requests at GitHub we have an agressive cache strategy. You have to wait 24 hours to our server fetch your data again.

To check your stats go to:
* Stats: https://awesome-github-stats.azurewebsites.net/user-stats/brunhbrito/stats
* Rank: https://awesome-github-stats.azurewebsites.net/user-stats/brunobritodev/rank

The magic🌟 begins from these endpoints! 

## 🤗 Contributing

Contributions are welcome! Feel free to open an issue or submit a pull request if you have a way to improve this project.

Make sure your request is meaningful and you have tested the app locally before submitting a pull request.

## 🤔 Motivations

Actually another repositories with the same goal doesn't count stats with accuracy. The main goal is to have a strong data first, then a beatiful svg.

## 🧰 Installing Requirements

#### Requirements

- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)

### Authorization

To get the GitHub API to run locally you will need to provide a token.

1. Go to <https://github.com/settings/tokens>
2. Click **"Generate new token"**
3. Add a note (ex. **"GitHub Pat"**), then scroll to the bottom and click **"Generate token"**
4. **Copy** the token to your clipboard
5. **Go to** file `appsettings.json` in the `src/AwesomeGithubStats.Api` directory and add your PAT at `PATS: [...]` with **your token**

```json
"PATS": [
  "your_pat_here"
]
```

### Running the tests

Before you can run tests, you have to add your PAT in `appsettings.json` at `tests/Tests`. Just how you have done above.

```bash
dotnet test
```

## 💕 Special thanks

Special thanks for [github-readme-streak-stats](https://github.com/DenverCoder1/github-readme-streak-stats) who build a Demo site. This version is almost the same code from there.

And another special thank to [Sindre Sorhus](https://github.com/sindresorhus) to increase the difficult to find a magical number to create Levels. The levels are based in his contributions.

----
## 🙋‍♂️ Support

💙 If you like this project, give it a ⭐ and share it with friends!

---

Made with .NET 8 ❤️
