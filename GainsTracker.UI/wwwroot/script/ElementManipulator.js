console.log("ElementManipulator initialized");

// The use of 'var' keywords instead of 'const' or 'let' in this file is necessary here for it to function.

var getHeadElement = function () {
    return document.getElementsByTagName("head")[0];
};

var toggleThemeStyleSheet = function (isDarkTheme) {
    let themeLink = getHeadElement().getElementsByClassName("theme")[0];
    let theme = isDarkTheme ? "darkTheme.css" : "lightTheme.css";
    themeLink.href = "_content/GainsTracker.UI/css/" + theme;
};