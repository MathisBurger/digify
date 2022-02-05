import React, { Component } from 'react';
import {BrowserRouter, Route, Switch} from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import DashboardPage from "./pages/DashboardPage";
import DataProvider from "./components/DataProvider";
import UsersPage from "./pages/UsersPage";
import Snackbar from "./components/Snackbar";

export default class App extends Component {
    static displayName = App.name;

    render () {
        return (
            <>
                <DataProvider>
                    <Snackbar>
                        <BrowserRouter>
                            <Switch>
                                <Route path="/login" component={LoginPage} />
                                <Route path="/dashboard" component={DashboardPage} />
                                <Route path="/user-management" component={UsersPage} />
                            </Switch>
                        </BrowserRouter>
                    </Snackbar>
                </DataProvider>
                
            </>
        );
    }
}