import React from 'react';
import { useEffect, useState } from "react";
import AuthenticationCard from '../../Components/RegistrationCard/RegistrationCard';

const HomePAGE: React.FC = () => {
    const [currentUser, setCurrentUser] = useState<string | null>(null);

    useEffect(() => {
        const savedUser = localStorage.getItem("currentUser");
        if (savedUser) {
            setCurrentUser(savedUser);
        }
    }, []);

    return (
        <div className="w-[100%] bg-[url(/public/bg1.jpg)] flex justify-center items-center bg-no-repeat bg-cover h-[100%] border-solid border-[0.2vw] border-[rgb(180,180,180)] rounded-[1vw] overflow-hidden">
            <div className="w-[110%] h-[110%]  backdrop-blur-[1vh] bg-[rgb(0,0,0,10%)] flex justify-center items-center rounded-[1vw]">  
                {currentUser ? (
                    <p>Добро пожаловать, {currentUser}!</p>
                ) : (
                    <AuthenticationCard />
                )}
            </div>
        </div>
    )
}

export default HomePAGE;