
![MultiWebViewBanner](https://user-images.githubusercontent.com/23462440/188020773-11165258-13f7-495b-b22a-890085a22a72.png)

![](https://img.shields.io/github/downloads/BadgerHobbs/MultiWebView/total)
![](https://img.shields.io/github/v/tag/BadgerHobbs/MultiWebView?label=Latest%20Release)

# MultiWebView
MultiWebView is a simple, fast, and configurable application for Windows which allows you to display any number of webviews in a single application window. Built using WPF, it uses Microsoft's latest WebView2 library to render webpages using the Microsoft Edge engine.

![image](https://user-images.githubusercontent.com/23462440/188022719-f9685696-8e09-48f5-af78-49816d6cfcb2.png)

## Features

- Unlimited configurable rows and columns.
- Automatic sizing of webviews to fill window.
- Configure any url to be rendered.
- Define the specific row and column for a url to be displayed.
- Lightweight and blazingly fast.
- Portable application, no install required!

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
