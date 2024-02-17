import { Routes, Route, BrowserRouter } from "react-router-dom";
import Home from "./scenes/home";
import Authorization from "./scenes/authorization";
import { ColorModeContext, useMode } from "./theme";
import { ThemeProvider } from "@mui/material";


function App() {
  const [theme, colorMode] = useMode();

  return (
    <ColorModeContext.Provider value={colorMode} >
      <ThemeProvider theme={theme}>
        <div className="app">
          <BrowserRouter>
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/login" element={<Authorization />} />
            </Routes>
          </BrowserRouter>
        </div>
      </ThemeProvider>
    </ColorModeContext.Provider>
  );
}

export default App;
