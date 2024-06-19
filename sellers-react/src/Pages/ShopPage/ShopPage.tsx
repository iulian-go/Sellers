import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom';
import { Shop } from '../../Models/Shop';
import { shopsGetById } from '../../Services/ShopsService';
import Card from '../../Components/Card/Card';
import { shopsConfig } from '../../Configs/ShopConfigs';

type Props = {}

const ShopPage = (props: Props) => {
    let {id} = useParams();
    const [shop, setShop] = useState<Shop>();

    useEffect(() => {
        const getShop = async () => {
            const result = await shopsGetById(id!);
            setShop(result?.data);
        };
        getShop();
    }, []);

  return (
    <div className='mx-auto'>
        {shop
        ? <Card config={shopsConfig} entity={shop} />
        : <h1>Loading</h1>}
    </div>
  )
}

export default ShopPage