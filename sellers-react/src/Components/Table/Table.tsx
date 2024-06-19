import { TableCell, TableHeadCell, TableRow, Table as FTable } from 'flowbite-react';
import React from 'react'

interface Props {
    config: any;
    data: any;
    va: any;
}

const Table = (props: Props) => {
  const renderHeader = props.config.map((config: any) => {
    return (
        <TableHeadCell key={config.label}>
            {config.label}
        </TableHeadCell>
    )
  });

  const renderRows = props.data.map((entity: any, index: number) => {
    return (
        <TableRow key={index}>
            {props.config.map((val: any, tdindex: number) => {
                return (
                    <TableCell key={tdindex}>
                        {val.render.length === 2 
                        ? val.render(entity, props.va) 
                        : val.render(entity)}
                    </TableCell>
                )
            })}
        </TableRow>
    )
  });

  return (
    <div className='overflow-x-auto shadow-md sm:rounded-lg mt-6'>
        <FTable hoverable>
            <FTable.Head>
                {renderHeader}
            </FTable.Head>
            <FTable.Body className='divide-y'>
                {renderRows}
            </FTable.Body>
        </FTable>
        {props.data == null || props.data.length === 0 
        ? <h1 className='mb-3 mt-3 text-xl text-center text-gray-600'>No data</h1> : ''}
    </div>
  )
}

export default Table