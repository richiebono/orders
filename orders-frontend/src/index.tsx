/* eslint react/jsx-key: off */
import * as React from 'react';
import { Admin, Resource, CustomRoutes } from 'react-admin'; // eslint-disable-line import/no-unresolved
import { render } from 'react-dom';
import { Route } from 'react-router-dom';
import authProvider from './authProvider';
import comments from './comments';
import CustomRouteLayout from './customRouteLayout';
import CustomRouteNoLayout from './customRouteNoLayout';
import dataProvider from './dataProvider';
import i18nProvider from './i18nProvider';
import Layout from './Layout';
import orders from './orders';
import users from './users';
import './style.css';

render(
    <React.StrictMode>
        <Admin
            authProvider={authProvider}
            dataProvider={dataProvider}
            i18nProvider={i18nProvider}
            title="Example Admin"
            layout={Layout}
        >
            <CustomRoutes noLayout>
                <Route
                    path="/custom"
                    element={<CustomRouteNoLayout title="orders from /custom" />}
                />
            </CustomRoutes>
            <Resource name="orders" {...orders} />
            {permissions => (
                <>
                    {permissions ? <Resource name="users" {...users} /> : null}
                    <CustomRoutes noLayout>
                        <Route
                            path="/custom1"
                            element={
                                <CustomRouteNoLayout title="orders from /custom1" />
                            }
                        />
                    </CustomRoutes>
                    <CustomRoutes>
                        <Route
                            path="/custom2"
                            element={
                                <CustomRouteLayout title="orders from /custom2" />
                            }
                        />
                    </CustomRoutes>
                </>
            )}
            <CustomRoutes>
                <Route
                    path="/custom3"
                    element={<CustomRouteLayout title="orders from /custom3" />}
                />
            </CustomRoutes>
        </Admin>
    </React.StrictMode>,
    document.getElementById('root')
);
