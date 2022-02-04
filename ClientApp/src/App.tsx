import React, { Component } from 'react';
import {BrowserRouter, Route, Switch} from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import DashboardPage from "./pages/DashboardPage";

export default class App extends Component {
    static displayName = App.name;

    render () {
        return (
            <BrowserRouter>
                <Switch>
                    <Route path="/login" component={LoginPage} />
                    <Route path="/dashboard" component={DashboardPage} />
                </Switch>
            </BrowserRouter>
        );
    }
}