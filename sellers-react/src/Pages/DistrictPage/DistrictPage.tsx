import { useEffect, useState } from 'react'
import { District } from '../../Models/District'
import { useParams } from 'react-router-dom'
import { districtsGetById } from '../../Services/DistrictsService';
import { Shop } from '../../Models/Shop';
import { shopsGetByDistrictId } from '../../Services/ShopsService';
import Table from '../../Components/Table/Table';
import { Vendor } from '../../Models/Vendor';
import { vendorsGetByDistrictId } from '../../Services/VendorsService';
import { VendorActions, vendorsConfig } from '../../Configs/VendorConfigs';
import { shopsConfig } from '../../Configs/ShopConfigs';
import { toast } from 'react-toastify';
import { assignmentsAssignVendor, assignmentsChangePrimary, assignmentsRemoveVendor } from '../../Services/VendorDistrictService';
import { Button, Drawer, DrawerHeader, DrawerItems } from 'flowbite-react';
import AutoComplete from '../../Components/AutoComplete/AutoComplete';

interface Props { }

const DistrictPage = (props: Props) => {
    let { id } = useParams();
    const [district, setDistrict] = useState<District>();
    const [shops, setShops] = useState<Shop>();
    const [vendors, setVendors] = useState<Vendor>();

    const getDistrictById = async () => {
        const result = await districtsGetById(id!);
        setDistrict(result?.data);
    };

    const getShopByDistrictId = async () => {
        const result = await shopsGetByDistrictId(id!);
        setShops(result?.data);
    };

    const getVendorsByDistrictId = async () => {
        const result = await vendorsGetByDistrictId(id!);
        setVendors(result?.data);
        setOnRefresh(true);
    };

    const onAssignVendor = (e: any) => {
        e.preventDefault();

        if (!selectedVendor){
            setIsError(true);
            return;
        }

        setIsError(false);
        closeDrawer();

        assignmentsAssignVendor({ vendorId: selectedVendor.id, districtId: Number(id) })
            .then(res => {
                if (res?.status === 200) {
                    toast.success(selectedVendor.firstName + " " + selectedVendor.lastName + " assigned succesfully");
                    getVendorsByDistrictId();
                }
            }).catch(e => toast.warning("Could not assign vendor"));

        if (e.target.role.value === 'primary') {
            changePrimaryVendor(selectedVendor.id);
        }

        setSelectedVendor(undefined);
        setOnRefresh(false);
    }

    const onVendorRemove = (e: any) => {
        e.preventDefault();
        assignmentsRemoveVendor({ vendorId: e.target[0].value, districtId: Number(id) })
            .then(res => {
                if (res?.status === 200) {
                    toast.success("Vendor removed from district");
                    getVendorsByDistrictId();
                }
            }).catch(e => toast.warning("Could not remove vendor"));
        setOnRefresh(false);
    }

    const changePrimaryVendor = (vendorId: number) => {
        assignmentsChangePrimary({ vendorId: vendorId, districtId: Number(id) })
            .then(res => {
                if (res?.status === 200) {
                    toast.success("Vendor changed to primary");
                    getVendorsByDistrictId();
                }
            }).catch(e => toast.warning("Could not change vendor"));
        setOnRefresh(false);
    }

    const onChangePrimary = (e: any) => {
        e.preventDefault();
        changePrimaryVendor(e.target[0].value);
    }

    const vendorActions = {} as VendorActions;
    vendorActions.onRemove = onVendorRemove;
    vendorActions.onChangePrimary = onChangePrimary;

    const [isOpen, setIsOpen] = useState(false);
    const openDrawer = () => setIsOpen(true);
    const closeDrawer = () => setIsOpen(false);

    const [onRefresh, setOnRefresh] = useState(false);
    const [isError, setIsError] = useState(false);
    const [selectedVendor, setSelectedVendor] = useState<Vendor>();
    const [selectedRole, setSelectedRole] = useState<string>();
    
    const handleSelectedVendor = (vendor: Vendor) => {
        setSelectedVendor(vendor);
        setIsError(false);
    }
    const handleSelectedRole = (e: any) => {
        setSelectedRole(e.target.value);
    }

    useEffect(() => {
        getDistrictById();
        getShopByDistrictId();
        getVendorsByDistrictId();
        // eslint-disable-next-line
    }, []);

    return (
        <>
            <div>
                <h2 className="mb-6 mt-6 text-3xl font-semibold md:text-4xl">
                    {district?.name} District
                </h2>

                <div className='relative flex items-center justify-between p-4'>
                    <h2 className="absolute left-1/2 transform -translate-x-1/2 mb-6 mt-6 text-3xl font-semibold text-center md:text-4xl">
                        VENDORS
                    </h2>
                    <Button className='ml-auto' onClick={openDrawer}>Assign Vendor</Button>
                </div>
                {vendors
                    ? <Table data={vendors} config={vendorsConfig} va={vendorActions} />
                    : <h1>Loading</h1>}

                <h2 className="mb-6 mt-6 text-3xl font-semibold text-center md:text-4xl">
                    SHOPS
                </h2>
                {shops
                    ? <Table data={shops} config={shopsConfig} va={undefined} />
                    : <h1>Loading</h1>}
            </div>

            <Drawer open={isOpen} onClose={closeDrawer} position='right'>
                <DrawerHeader title='ASSIGN VENDOR' />
                <DrawerItems>
                    <form onSubmit={onAssignVendor} className='flex max-w-md flex-col gap-4'>
                        <AutoComplete onRefresh={onRefresh} districtId={Number(id)} isError={isError} onVendorSelect={handleSelectedVendor} />
                        <select name="role" defaultValue="secondary" value={selectedRole} onChange={handleSelectedRole}>
                            <option value="primary">Primary</option>
                            <option value="secondary">Secondary</option>
                        </select>
                        {selectedRole === 'primary' && (
                            <p className='text-sm text-red-500'>Will override the primary vendor</p>
                        )}
                        <Button type='submit'>Assign</Button>
                    </form>
                </DrawerItems>
            </Drawer>
        </>
    )
}

export default DistrictPage