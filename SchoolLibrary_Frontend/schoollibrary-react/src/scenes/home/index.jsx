import { useTheme } from "@mui/material";
import Box from "@mui/material/Box";
import { tokens } from "../../theme";
import HeaderBar from "../global/HeaderBar";
import FooterBar from "../global/FooterBar";


const Home = () => {
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);

    return (
        <Box
            sx={{
                width: 1,
                backgroundColor: colors.primary[700],
                position: "relative",
            }} 
        >
            <HeaderBar logoText="SchoolLibrary" />

            <Box color={colors.gray[900]}>
                HomePaige
            </Box>

            <FooterBar logoText="SchoolLibrary" />
        </Box>
    );
};

export default Home;