import { Link } from "react-router-dom";
import { Shop } from "../Models/Shop";

export const shopsConfig = [
    {
        label: "Name",
        render: (shop: Shop) =>
            <Link className="whitespace-nowrap font-semibold hover:text-teal-500" to={`/shops/${shop.id}`}>{shop.name}</Link>
    },
    {
        label: "Address",
        render: (shop: Shop) => shop.address
    },
    {
        label: "Type",
        render: (shop: Shop) => shop.shopType
    }
];