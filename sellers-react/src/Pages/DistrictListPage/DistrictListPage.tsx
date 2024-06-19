import React, { useEffect, useState } from 'react'
import Table from '../../Components/Table/Table'
import { District } from '../../Models/District'
import { districtsGetAll } from '../../Services/DistrictsService'
import { districtsConfig } from '../../Configs/DistrictConfigs'

type Props = {}

const DistrictListPage = (props: Props) => {
    const [districts, setDistricts] = useState<District[]>();

    useEffect(() => {
        const getDistricts = async () => {
            const result = await districtsGetAll();
            setDistricts(result?.data);
        };
        getDistricts();
    }, []);

  return (
    <>
        <h2 className="mb-6 mt-6 text-3xl font-semibold text-center md:text-4xl">
            DISTRICTS
        </h2>
        {districts
        ? <Table data={districts} config={districtsConfig} va={undefined} />
        : <h1>Loading</h1>}
    </>
  )
}

export default DistrictListPage