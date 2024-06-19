import React from 'react'
import './ErrorBoundary.css';
import { isRouteErrorResponse, useRouteError } from 'react-router'

type Props = {}

const ErrorBoundary = (props: Props) => {
    const error = useRouteError();

    if (!isRouteErrorResponse(error)) return <h2>Something went wrong</h2>;

    const code = error.status.toString();
    const renderChar = Array.from(code).map((char, index) => {
        return <span key={index}>{char}</span>
    });

    return (
        <div className='notfound'>
            <h1>{renderChar}</h1>
            <h2>{error.statusText}</h2>
        </div>
    )
}

export default ErrorBoundary