import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { Vendor } from '../../Models/Vendor';
import { vendorsGetById } from '../../Services/VendorsService';
import Card from '../../Components/Card/Card';
import { vendorsConfig } from '../../Configs/VendorConfigs';

type Props = {}

const VendorPage = (props: Props) => {
    let {id} = useParams();
    const [vendor, setVendor] = useState<Vendor>();

    useEffect(() => {
        const getVendor = async () => {
            const result = await vendorsGetById(id!);
            setVendor(result?.data);
        };
        getVendor();
    }, []);

  return (
    <div className='mx-auto'>
        {vendor
        ? <Card config={vendorsConfig} entity={vendor} />
        : <h1>Loading</h1>}
    </div>
  )
}

export default VendorPage