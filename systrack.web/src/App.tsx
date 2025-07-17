import React, { cache } from 'react';
import './App.css';
import { Outlet } from 'react-router';
import { useEffect, useState } from 'react';
import Sidebar from './Components/SideBar/SideBar';

function App() {

    const [currentUser, setCurrentUser] = useState<string | null>(null);

    useEffect(() => {
        const savedUser = localStorage.getItem("currentUser");
        if (savedUser) {
            setCurrentUser(savedUser);
        }
    }, []);

  return (
    <div className="App flex justify-center items-center w-[100vw] h-[100vh] bg-[rgb(220,220,220)] p-[1vw] gap-[1vw]">
          {currentUser ? (
              <>
                  <Sidebar />
                  <Outlet />
              </>
          ) : (
              <Outlet />
          )}
    </div>
  );
}

export default App;
