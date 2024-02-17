import { Box, List, ListItem, Typography, useTheme } from "@mui/material";
import { ColorModeContext, tokens } from "../../theme";
import { useContext } from "react";
import { Link } from "react-router-dom";



const HeaderBar = ({ logoText }) => {
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);
    const colorMode = useContext(ColorModeContext);

    return (
        <Box 
            sx={{
                backgroundColor: colors.primary[800],
                height: "100px",
                width: 1,
                "& img": {
                    userSelect: "none",
                    msUserSelect: "none",
                    MozUserSelect: "none",
                },
            }}
        >
            <div className="__container" style={{height: "100%"}}>
                <Box 
                    sx={{
                        height: 1,
                        display: "flex",
                        justifyContent: "space-between"
                    }}
                >
                    {/* LogoBar */}
                    <Box 
                        sx={{
                            display: "flex",
                            alignItems: "center",
                            maxWidth: "270px",
                            height: 1,
                            width: 1,
                            paddingLeft: "25px"
                        }}
                    >
                        <Link to="/" style={{ display: "flex" }}>
                            <img 
                                alt="LogoIcon"
                                width="30px"
                                height="30px"
                                src={`../../assets/logo-icon.png`}
                                style={{ cursor: "pointer" }}
                            />

                            <Typography 
                                variant="h1"
                                sx={{
                                    color: colors.primary[100],
                                    fontFamily: "Arial, sans-serif",
                                    pl: "10px",
                                    textDecoration: "underline"
                                }}
                            >
                                {logoText}
                            </Typography>
                        </Link>
                    </Box>


                    {/* MenuBar */}
                    <Box 
                        sx={{
                            width: "50%",
                            color: colors.primary[100],
                            height: "100px",
                        }}
                    >
                        <List 
                            sx={{
                                height: 1,
                                p: 0,
                                display: "flex", 
                                flexDirection: "row",
                                justifyContent: "space-between",
                                "& .MuiBox-root": {
                                    padding: "10px",
                                    borderRadius: "3px",
                                },
                                "& .MuiListItem-root .MuiBox-root": {
                                    transition: ".3s",
                                },
                                "& .MuiListItem-root .MuiBox-root:hover": {
                                    backgroundColor: colors.primary[500],
                                }
                            }}
                        >
                            <ListItem
                                style={{
                                    display: "flex",
                                    alignItems: "center",
                                    justifyContent: "center"
                                }}
                            >
                                <Box sx={{display: "flex", alignItems: "center", cursor: "pointer"}}>
                                    <img 
                                        alt="menu-icon"
                                        width="15"
                                        height="15"
                                        src={`../../assets/menu-icon.png`}
                                    />

                                    <Typography variant="h3" sx={{fontFamily: "Arial, sans-serif", ml: "10px"}}>
                                        Catalog
                                    </Typography>
                                </Box>
                            </ListItem>

                            <ListItem
                                style={{
                                    display: "flex",
                                    alignItems: "center",
                                    justifyContent: "center"
                                }}
                            >
                                <Box sx={{display: "flex", alignItems: "center", cursor: "pointer"}}>
                                    <img 
                                        alt="search-icon"
                                        width="15"
                                        height="15"
                                        src={`../../assets/search-icon.png`}
                                    />

                                    <Typography variant="h3" sx={{fontFamily: "Arial, sans-serif", ml: "10px"}}>
                                        Search
                                    </Typography>
                                </Box>
                            </ListItem>

                            <ListItem
                                style={{
                                    display: "flex",
                                    alignItems: "center",
                                    justifyContent: "center"
                                }}
                            >
                                <Link to="/forum" style={{textDecoration: "none"}}>
                                    <Box 
                                        sx={{
                                            display: "flex", 
                                            alignItems: "center", 
                                            cursor: "pointer",
                                            color: colors.primary[100],
                                        }}
                                    >
                                        <img 
                                            alt="forum-icon"
                                            width="15"
                                            height="15"
                                            src={`../../assets/forum-icon.png`}
                                        />

                                        <Typography variant="h3" sx={{fontFamily: "Arial, sans-serif", ml: "10px"}}>
                                            Forum
                                        </Typography>
                                    </Box>
                                </Link>
                            </ListItem>

                            <ListItem
                                style={{
                                    display: "flex",
                                    alignItems: "center",
                                    justifyContent: "center"
                                }}
                            >
                                <Link to="/faq" style={{textDecoration: "none"}}>
                                    <Box 
                                        sx={{
                                            display: "flex", 
                                            alignItems: "center", 
                                            cursor: "pointer",
                                            color: colors.primary[100],
                                        }}
                                    >
                                        <img 
                                            alt="faq-icon"
                                            width="15"
                                            height="15"
                                            src={`../../assets/faq-icon.png`}
                                        />

                                        <Typography variant="h3" sx={{fontFamily: "Arial, sans-serif", ml: "10px"}}>
                                            FAQ
                                        </Typography>
                                    </Box>
                                </Link>
                            </ListItem>

                            <ListItem 
                                style={{
                                    display: "flex",
                                    alignItems: "center",
                                    justifyContent: "center"
                                }}
                            >
                                <Box
                                    sx={{
                                        display: "flex", 
                                        alignItems: "center", 
                                        cursor: "pointer",
                                    }}
                                >
                                    <img 
                                        alt="dots-icon"
                                        width="28"
                                        height="28"
                                        src={`../../assets/dots-icon.png`}
                                    />
                                </Box>
                            </ListItem>
                        </List>
                    </Box>

                    
                    {/* AccountBar */}
                    <Box
                        sx={{
                            height: 1,
                            width: "26.5%",
                            paddingRight: "25px",
                            display: "flex",
                            flexDirection: "row",
                            alignItems: "center",
                            justifyContent: "end",
                            "& .MuiBox-root": {
                                transition: ".3s",
                                margin: "0 0 0 10px",
                                padding: "0px 11px",
                                height: "36px",
                                display: "flex",
                                alignItems: "center",
                                borderRadius: "6px",
                                cursor: "pointer",
                            },
                            "& .MuiBox-root:hover": {
                                backgroundColor: colors.primary[500],
                            },
                            "& .MuiTypography-root": {
                                color: `${colors.primary[100]} !important`,
                            },
                        }}
                    >
                        <Box>
                            <Link to="/login" style={{textDecoration: "none"}}>
                                <Typography variant="h5" fontSize="18px" fontFamily="Arial, sans-serif">
                                    Login
                                </Typography>
                            </Link>
                        </Box>

                        <Box sx={{border: "1px solid #fff"}}>
                            <Link to="/registration" style={{textDecoration: "none"}}>
                                <Typography variant="h5" fontSize="18px" fontFamily="Arial, sans-serif">
                                    Registration
                                </Typography>
                            </Link>
                        </Box>

                        <Box onClick={colorMode.toggleColorMode}>
                            <img
                                alt="theme-icon"
                                height="16"
                                width="16"
                                src={`../../assets/theme-icon.png`} 
                            />
                        </Box>
                    </Box>
                </Box>
            </div>
        </Box>
    );
};

export default HeaderBar;