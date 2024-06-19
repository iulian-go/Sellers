import { Badge, FloatingLabel } from 'flowbite-react';
import React, { useState, useEffect, useRef } from 'react';
import { vendorsGetAll } from '../../Services/VendorsService';
import { Vendor } from '../../Models/Vendor';
import { toPascalCase } from '../../Helpers/StringFormatting';

interface Props {
    districtId: number;
    isError: boolean;
    onRefresh: boolean;
    onVendorSelect: (vendor: Vendor) => void;
}

const AutoComplete = (props: Props) => {
    const [searchTerm, setSearchTerm] = useState('');
    const [vendors, setVendors] = useState<Vendor[]>();
    const [showSuggestions, setShowSuggestions] = useState(true);
    const [filteredVendors, setFilteredVendors] = useState(vendors);
    const [selectedVendor, setSelectedVendor] = useState<Vendor>();
    const containerRef = useRef<HTMLDivElement>(null);

    const onListItemClick = (vendor: Vendor) => {
        setSearchTerm('');
        setSelectedVendor(vendor);
        setShowSuggestions(false);
        props.onVendorSelect(vendor);
    }

    const getAllVendors = async () => {
        const result = await vendorsGetAll();
        setVendors(result?.data);
        setSelectedVendor(undefined);
    }

    useEffect(() => {
        getAllVendors();
    }, [props.onRefresh]);

    useEffect(() => {
        setFilteredVendors(
            vendors?.filter(vendor =>
                vendor.districts.every(d => d.id !== props.districtId) &&
                (vendor.firstName.toLowerCase().includes(searchTerm.toLowerCase()) ||
                    vendor.lastName.toLowerCase().includes(searchTerm.toLowerCase()))
            )
        );
    }, [vendors, searchTerm, props.districtId]);

    useEffect(() => {
        const handleClickOutside = (event: any) => {
            if (containerRef.current && !containerRef.current.contains(event.target)) {
                setShowSuggestions(false);
            }
        };

        document.addEventListener('mousedown', handleClickOutside);
        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
    }, []);

    const renderVendor = (vendor: Vendor) => {
        return (
            <li key={vendor.id} onClick={() => { onListItemClick(vendor); }}>
                <p className="group flex items-center px-4 py-2 text-base font-bold text-gray-900 hover:bg-blue-100 hover:shadow cursor-pointer">
                    <span className="ml-3 flex-1 whitespace-nowrap">{vendor.firstName} {vendor.lastName}</span>
                    <Badge className='inline' color={vendor.role === 'primary' ? "pink" : "indigo"}>{toPascalCase(vendor.role)}</Badge>
                </p>
            </li>
        )
    };

    return (
        <div className="w-full max-w-md mx-auto mt-10">
            <div className="relative" ref={containerRef}>
                <FloatingLabel variant='standard' label='Search vendors...'
                    type="text"
                    color={props.isError ? 'error' : 'default'}
                    value={searchTerm}
                    onChange={e => { setSearchTerm(e.target.value); setShowSuggestions(true); }}
                    onFocus={e => setShowSuggestions(true)}
                />
                {showSuggestions && (
                    <ul className="absolute z-10 w-full bg-white border border-gray-300 mt-1 rounded shadow-lg max-h-60 overflow-y-auto">
                        {filteredVendors != null && filteredVendors.length > 0 ? (
                            filteredVendors.map(vendor => renderVendor(vendor))
                        ) : (
                            <li className="px-4 py-2 text-gray-500">No vendors found</li>
                        )}
                    </ul>
                )}
                <ul className='bg-blue-50'>
                    {selectedVendor && (renderVendor(selectedVendor))}
                </ul>
            </div>
        </div>
    );
};

export default AutoComplete;
