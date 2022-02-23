import React, { Component } from 'react';
import {BrowserRouter, Route, Switch} from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import DashboardPage from "./pages/DashboardPage";
import DataProvider from "./components/DataProvider";
import UsersPage from "./pages/UsersPage";
import Snackbar from "./components/Snackbar";
import ClassesPage from "./pages/ClassesPage";
import TimetablePage from "./pages/TimetablePage";
import AdapterDateFns from "@mui/lab/AdapterDateFns";
import {LocalizationProvider} from "@mui/lab";

export default class App extends Component {
    static displayName = App.name;

    render () {
        return (
            <>
                <LocalizationProvider dateAdapter={AdapterDateFns}>
                    <DataProvider>
                        <Snackbar>
                            <BrowserRouter>
                                <Switch>
                                    <Route path="/login" component={LoginPage} />
                                    <Route path="/dashboard" component={DashboardPage} />
                                    <Route path="/user-management" component={UsersPage} />
                                    <Route path="/classes" component={ClassesPage} />
                                    <Route path="/timetable" component={TimetablePage} />
                                </Switch>
                            </BrowserRouter>
                        </Snackbar>
                    </DataProvider>
                </LocalizationProvider>
            </>
        );
    }
}