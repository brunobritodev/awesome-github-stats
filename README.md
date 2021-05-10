<p align="center">
  <img src="https://raw.githubusercontent.com/brunohbrito/awesome-github-stats/master/docs/banner.png"/>
</p>

<p align="center">
  Display an accurate data from your contributions
  <br/>
  into a card to show at your GitHub profile README
</p>

## 🚀 Quick setup

1. Copy-paste the markdown below into your GitHub profile README
2. Replace the value after `brunohbrito` with your GitHub username

```md
[![My Awesome Stats](https://awesome-github-stats.azurewebsites.net/user-stats/brunohbrito)](https://git.io/awesome-stats-card)
```
## 💻 Demo Site

Here you can customize your Card with a live preview: <https://awesome-github-stats.azurewebsites.net/>

[![Demo Site](https://raw.githubusercontent.com/brunohbrito/awesome-github-stats/master/docs/awesomestats.gif "Demo Site")](https://awesome-github-stats.azurewebsites.net/)

## 🎨 Themes

To enable a theme, append `&theme=` followed by the theme name to the end of the source url:

```md
[![GitHub Streak](https://awesome-github-stats.azurewebsites.net/user-stats/brunohbrito&theme=dark)](https://git.io/awesome-stats-card)
```

|    Theme     |                                                 Preview                                                 |
| :----------: | :-----------------------------------------------------------------------------------------------------: |
|  `default`   |          ![default](https://awesome-github-stats.azurewebsites.net/user-stats/ralmsdeveloper)           |
|    `dark`    |       ![dark](https://awesome-github-stats.azurewebsites.net/user-stats/eduardopires?theme=dark)        |
| `tokyonight` | ![highcontrast](https://awesome-github-stats.azurewebsites.net/user-stats/brunohbrito?theme=tokyonight) |
| More themes! |                   **🎨 [See a list of all available themes](./docs/themes/README.md)**                   |

> If you have come up with a new theme you'd like to share with others, open an issue to add it!

## 🔧 Options

The `user` field is part of url `user-stats/<user>`, which is required. All other fields are optional.

If the `theme` parameter is specified, any color customizations specified will be applied on top of the theme, overriding the theme's values.

|  Parameter   |                       Details                        |                        Example                        |
| :----------: | :--------------------------------------------------: | :---------------------------------------------------: |
|   `theme`    |       The theme to apply (Default: `default`)        | `dark`, `radical`, etc. [🎨➜](./docs/themes/README.md) |
| `show_icons` | Dont shown icons at left of labels (Default: `true`) |                   `true` or `false`                   |
| `background` |                   Background color                   |      **hex code** (without `#`) or **css color**      |
|   `border`   |                     Border color                     |      **hex code** (without `#`) or **css color**      |
|    `text`    |                  Color of the text                   |      **hex code** (without `#`) or **css color**      |
|   `title`    |              Color of the title at top               |      **hex code** (without `#`) or **css color**      |
|    `icon`    |                  Color of the icons                  |      **hex code** (without `#`) or **css color**      |
|    `ring`    |          Color of the ring around the level          |      **hex code** (without `#`) or **css color**      |


### Example

```md
[![My Awesome Custom Stats](https://localhost:5001/user-stats/brunohbrito?theme=tokyonight&Ring=DD2727&Border=13DD57&Text=DD2727)](https://git.io/awesome-stats-card)
```

## ℹ️ How these stats are calculated

This tool uses the contribution graphs on your GitHub profile to calculate which days you have contributed.

To include contributions in private repositories, turn on the setting for "Private contributions" from the dropdown menu above the contribution graph on your profile page.

Contributions include commits, pull requests, and issues that you create in standalone repositories ([Learn more about what is considered a contribution](https://docs.github.com/articles/why-are-my-contributions-not-showing-up-on-my-profile)).

The longest streak is the highest number of consecutive days on which you have made at least one contribution.

The current streak is the number of consecutive days ending with the current day on which you have made at least one contribution. If you have made a contribution today, it will be counted towards the current streak, however, if you have not made a contribution today, the streak will only count days before today so that your streak will not be zero.

> Note: You may need to wait up to 24 hours for new contributions to show up ([Learn how contributions are counted](https://docs.github.com/articles/why-are-my-contributions-not-showing-up-on-my-profile))

## 🤗 Contributing

Contributions are welcome! Feel free to open an issue or submit a pull request if you have a way to improve this project.

Make sure your request is meaningful and you have tested the app locally before submitting a pull request.

### Installing Requirements

#### Requirements

- [.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)

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


## 🙋‍♂️ Support

💙 If you like this project, give it a ⭐ and share it with friends!

---

Made .NET 5 ❤️
