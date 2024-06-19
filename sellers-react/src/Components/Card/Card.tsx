import React from 'react'
import { Card as FCard } from 'flowbite-react';

interface Props {
    config: any;
    entity: any;
}

const Card = (props: Props) => {
    const renderList = props.config.map((config: any, index: number) => {
        if (index === 0 || config.render.length === 2) return (<></>);
        return (
            <li>
                <p className="group flex items-center rounded-lg bg-gray-50 p-3 text-base font-bold text-gray-900 hover:bg-gray-100 hover:shadow">
                    <span className="ml-3 flex-1 whitespace-nowrap">{config.label}</span>
                    <span className="ml-3 flex items-center justify-center  px-2 py-0.5 font-medium text-gray-500">
                        {config.render(props.entity)}
                    </span>
                </p>
            </li>
        )
    });

    return (
        <FCard className='max-w-sm'>
            <h5 className="mx-4 text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
                {props.config[0].render(props.entity)}
            </h5>
            <ul className="my-4 space-y-3">
                {renderList}
            </ul>
        </FCard>
    )
}

export default Card