import { createBrowserRouter } from 'react-router-dom';
import App from '../App';
import HomePAGE from '../Pages/HomePAGE/HomePAGE';
import ConnectionsPAGE from '../Pages/ConnectionsPAGE/ConnectionsPAGE';


export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            { path: "/", element: <HomePAGE /> },
            { path: "/Connections", element: <ConnectionsPAGE /> },
        ]
    }
])