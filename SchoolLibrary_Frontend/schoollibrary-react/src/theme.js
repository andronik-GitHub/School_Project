import { createContext, useState, useMemo } from "react";
import { createTheme } from "@mui/material";

// #FFFFFF,
// #DCDCDC,
// #D9D9D9,
// #CDCDCD,
// #A0A0A0,
// #9C9C9C,
// #8A8A8A,
// #818181,
// #535353,
// #313131,
// #333335,
// #272729,
// #141414,
// #1C1C1C,
// #000000

// color design tokens
export const tokens = (mode) => ({
    ...(mode === "dark"
    ? // if dark
    {
        gray: {
            100: "#000000",
            200: "#cdcdcd",
            300: "#ffffff",
            400: "#9c9c9c",
            500: "#8a8a8a",
            600: "#818181",
            700: "#1c1c1c",
            800: "#313131",
            900: "#ffffff",
        },
        primary: {
            100: "#ffffff",
            200: "#d9d9d9",
            300: "#e48f13",
            400: "#e47f09",
            500: "#353535",
            600: "#272729",
            700: "#141414",
            800: "#1c1c1c",
            900: "#000000",
        }
    }
    : // else light
    {
        gray: {
            100: "#ffffff",
            200: "#313131",
            300: "#000000",
            400: "#818181",
            500: "#8a8a8a",
            600: "#9c9c9c",
            700: "#ffffff",
            800: "#cdcdcd",
            900: "#000000",
        },
        primary: {
            100: "#ffffff",
            200: "#1c1c1c",
            300: "#141414",
            400: "#e47f09",
            500: "#f2a32e",
            600: "#d9d9d9",
            700: "#f2f2f3",
            800: "#e48f13",
            900: "#000000",
        }
    })
});


// mui theme settings
export const themeSettings = (mode) => {
    const colors = tokens(mode);

    return {
        palette: {
            mode: mode,
            ...(mode === "dark"
            ? // if
            {
                // palette values for dark mode
                primary: {
                    main: colors.primary[500],
                },
                secondary: {
                    main: colors.primary[500],
                },
                neutral: {
                    dark: colors.primary[700],
                    main: colors.primary[500],
                    light: colors.primary[100]
                },
                background: {
                    default: colors.primary[500],
                }
            }
            : // else
            {
                // palette values for light mode
                primary: {
                    main: colors.primary[100],
                },
                secondary: {
                    main: colors.primary[500],
                },
                neutral: {
                    dark: colors.primary[700],
                    main: colors.primary[500],
                    light: colors.primary[100]
                },
                background: {
                    default: "#fcfcfc",
                }
            }),
        },
        typography: {
            fontFamily: [
                "Inter", 
                "Arial", 
                "sans-serif"
            ].join(","),
            fontSize: 12,
            h1: {
                fontFamily: ["Inter", "sans-serif"].join(","),
                fontSize: 28,
            },
            h2: {
                fontFamily: ["Inter", "sans-serif"].join(","),
                fontSize: 24,
            },
            h3: {
                fontFamily: ["Inter", "sans-serif"].join(","),
                fontSize: 22,
            },
            h4: {
                fontFamily: ["Inter", "sans-serif"].join(","),
                fontSize: 20,
            },
            h5: {
                fontFamily: ["Inter", "sans-serif"].join(","),
                fontSize: 16,
            },
            h6: {
                fontFamily: ["Inter", "sans-serif"].join(","),
                fontSize: 14,
            },
        },
    };
};


// context for color mode
export const ColorModeContext = createContext({
    toggleColorMode: () => {},
});


export const useMode = () => {
    const [mode, setMode] = useState("dark");

    const colorMode = useMemo(
        () => ({
            toggleColorMode: () => setMode((prev) => (prev === "light" ? "dark" : "light")),
        }), 
        []
    );

    const theme = useMemo(() => createTheme(themeSettings(mode)), [mode]);

    return [theme, colorMode];
};