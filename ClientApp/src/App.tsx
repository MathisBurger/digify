import React, { Component } from 'react';
import {BrowserRouter, Route, Switch} from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import DashboardPage from "./pages/DashboardPage";
import DataProvider from "./components/DataProvider";

export default class App extends Component {
    static displayName = App.name;

    render () {
        return (
            <DataProvider>
                <BrowserRouter>
                    <Switch>
                        <Route path="/login" component={LoginPage} />
                        <Route path="/dashboard" component={DashboardPage} />
                    </Switch>
                </BrowserRouter>
            </DataProvider>
        );
    }
}