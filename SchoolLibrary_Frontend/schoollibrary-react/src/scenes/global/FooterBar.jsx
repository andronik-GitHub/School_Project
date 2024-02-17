import { Box, List, ListItem, Tooltip, Typography, useTheme } from "@mui/material";
import { ColorModeContext, tokens } from "../../theme";
import { useContext } from "react";
import { Link } from "react-router-dom";



const FooterBar = ({ logoText }) => {
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);
    const colorMode = useContext(ColorModeContext);

    const year = new Date().getFullYear();
    const email = "info@schoollibrary.me";
    const copyrightText = `In cases of copyright infringement - please contact ${email}`;

    return (
        <Box
            sx={{
                backgroundColor: colors.primary[800],
                maxHeight: "150px",
                height: 1,
                width: 1,
                position: "fixed",
                bottom: 0,
                "& img": {
                    userSelect: "none",
                    msUserSelect: "none",
                    MozUserSelect: "none",
                },
            }}
        >
            <div className="__container" style={{ height: "100%" }}>
                <Box
                    sx={{
                        height: 1,
                        color: colors.primary[100],
                        display: "flex",
                        justifyContent: "space-between",
                        "& a": {
                            color: colors.primary[400],
                            textDecoration: "none",
                        },
                        "& a:hover": {
                            textDecoration: "underline !important",
                        },
                    }}
                >
                    {/* Main info */}
                    <Box 
                        sx={{
                            margin: "15px 0px 25px 15px",
                            width: "65%",
                            display: "flex",
                            flexDirection: "column",
                            justifyContent: "space-between",
                        }}
                    >
                        {/* Themes */}
                        <Box sx={{display: "flex"}}>
                            <Typography variant="h6" fontSize="32px" fontFamily="Arial, sans-serif">Theme</Typography>

                            <Box
                                sx={{
                                    maxWidth: "95px",
                                    width: 1,
                                    marginLeft: "20px",
                                    display: "flex",
                                    justifyContent: "space-between",
                                    userSelect: "none",
                                    
                                }}
                            >
                                <Tooltip title="Light theme">
                                    <Box 
                                        onClick={theme.palette.mode === "dark" ? colorMode.toggleColorMode : undefined}
                                        sx={{
                                            width: "40px",
                                            height: "40px",
                                            backgroundColor: "#fff",
                                            color: "#000",
                                            border: "1px solid #8a8a8a",
                                            borderRadius: "3px",
                                            display: "flex",
                                            alignItems: "end",
                                            cursor: "pointer",
                                            fontSize: "22px",
                                            fontFamily: "Arial, sans-serif",
                                        }}
                                    >
                                        <span style={{marginLeft: "5px"}}>A</span>
                                    </Box>
                                </Tooltip>

                                <Tooltip title="Dark theme">
                                    <Box 
                                        onClick={theme.palette.mode === "light" ? colorMode.toggleColorMode : undefined}
                                        sx={{
                                            width: "40px",
                                            height: "40px",
                                            backgroundColor: "#000",
                                            color: "#fff",
                                            border: "1px solid #8a8a8a",
                                            borderRadius: "3px",
                                            display: "flex",
                                            alignItems: "end",
                                            cursor: "pointer",
                                            fontSize: "22px",
                                            fontFamily: "Arial, sans-serif",
                                        }}
                                    >
                                        <span style={{marginLeft: "5px"}}>A</span>
                                    </Box>
                                </Tooltip>
                            </Box>
                        </Box>

                        {/* Corypight */}
                        <Box>
                            <Typography variant="h3" fontFamily="Arial, sans-serif">
                                {copyrightText}
                            </Typography>
                        </Box>

                        {/* About project */}
                        <Box>
                            <List
                                sx={{
                                    padding: 0,
                                    display: "flex",
                                    "& .MuiListItem-root": {
                                        padding: 0,
                                        fontSize: "22px",
                                        fontFamily: "Arial, sans-serif",
                                        width: "auto",
                                    },
                                }}
                            >
                                {Array.from({ length: 5 }).map((_, index) => {
                                    return (
                                        <ListItem key={index}>
                                            <Link
                                                to={
                                                    index === 0 ? "feed-back" :
                                                    index === 1 ? "for-copyright-holders" :
                                                    index === 2 ? "terms-of-use" :
                                                    index === 3 ? "dmca" :
                                                    index === 4 ? "license" : ""
                                                }
                                            >
                                                {
                                                    index === 0 ? "FeedBack" :
                                                    index === 1 ? "For copyright holders" :
                                                    index === 2 ? "Terms of use" :
                                                    index === 3 ? "DMCA" :
                                                    index === 4 ? "License" : ""
                                                }
                                            </Link>

                                            {/* Dots between items */
                                            index >= 4 ? (<span></span>) : 
                                            (
                                                <Box sx={{display: "flex", justifyContent: "center", alignItems: "center"}}>
                                                    <div
                                                        style={{
                                                            height: "5px",
                                                            width: "5px",
                                                            background: "#fff",
                                                            margin: "0 5px",
                                                            borderRadius: 100,
                                                        }}
                                                    >
                                                    </div>
                                                </Box>
                                            )
                                            }
                                        </ListItem>
                                    );
                                })}
                            </List>
                        </Box>
                    </Box>

                    {/* Second info */}
                    <Box 
                        sx={{
                            margin: "10px 15px 25px 0px",
                            display: "flex", 
                            flexDirection: "column", 
                            justifyContent: "end"
                        }}
                    >
                        <Typography variant="h3">
                            <span>© {year}</span>
                            <Link to="/" style={{ marginLeft: "10px" }}>
                                {logoText}
                            </Link>
                        </Typography>
                    </Box>
                </Box>
            </div>
        </Box>
    );
};

export default FooterBar;