import React, { SyntheticEvent } from 'react'
import { Button } from 'flowbite-react';

interface Props {
    name: string;
    id: number;
    onAction: (e: SyntheticEvent) => void;
    color?: string | undefined;
}

const FormButton = (props: Props) => {
  return (
    <div>
        <form onSubmit={props.onAction}>
            <input readOnly hidden value={props.id} />
            <Button type='submit' size='xs' color={props.color}>{props.name}</Button>
        </form>
    </div>
  )
}

export default FormButton