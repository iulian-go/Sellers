import { ToastContainer } from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";
import './App.css';
import { Outlet } from 'react-router-dom';
import { Navbar } from 'flowbite-react';

function App() {
  return (
    <>
      <Navbar fluid className='bg-gray-50' border>
        <Navbar.Toggle />
        <Navbar.Collapse>
          <Navbar.Link href='/districts' className='text-xl'>Districts</Navbar.Link>
        </Navbar.Collapse>
      </Navbar>

      <div className='relative container mx-auto p-6'>
        <Outlet />
      </div>
      <ToastContainer />
    </>
  );
}

export default App;
