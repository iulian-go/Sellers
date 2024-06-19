import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import ErrorBoundary from "../Components/Error/ErrorBoundary";
import DistrictListPage from "../Pages/DistrictListPage/DistrictListPage";
import DistrictPage from "../Pages/DistrictPage/DistrictPage";
import VendorPage from "../Pages/VendorPage/VendorPage";
import ShopPage from "../Pages/ShopPage/ShopPage";

export const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        errorElement: <ErrorBoundary/>,
        children: [
            {path: "", element: <DistrictListPage />},
            {path: "districts", element: <DistrictListPage />},
            {path: "districts/:id", element: <DistrictPage />},
            {path: "vendors/:id", element: <VendorPage />},
            {path: "shops/:id", element: <ShopPage />}
        ]
    }
]);