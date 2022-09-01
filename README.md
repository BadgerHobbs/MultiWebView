
![MultiWebViewBanner](https://user-images.githubusercontent.com/23462440/188020773-11165258-13f7-495b-b22a-890085a22a72.png)

# MultiWebView
MultiWebView is a simple, fast and configurable application for Windows which allows you to display any number of webviews in a single application window.

## Getting Started
Download or build the latest release, then either create or modify the existing `config.json` file (generated on first launch). Below is an example configuration.

```json
{
  "WebViews": [
    {
      "Url": "https://github.com/BadgerHobbs/MultiWebView",
      "Row": 1,
      "Column": 1
    },
    {
      "Url": "https://www.reddit.com/r/CasualUK/",
      "Row": 1,
      "Column": 2
    },
    {
      "Url": "https://stackoverflow.com/",
      "Row": 2,
      "Column": 1
    },
    {
      "Url": "https://http.cat/",
      "Row": 2,
      "Column": 2
    }
  ],
  "Rows": 2,
  "Columns": 2,
  "ShowGridLines": false
}
```
