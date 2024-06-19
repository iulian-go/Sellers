import { Link } from "react-router-dom";
import { District } from "../Models/District";

export const districtsConfig = [
    {
        label: "Name",
        render: (district: District) => 
            <Link className="whitespace-nowrap font-semibold hover:text-teal-500" to={`/districts/${district.id}`}>{district.name}</Link>
    },
    {
        label: "City",
        render: (district: District) => district.city
    }
]