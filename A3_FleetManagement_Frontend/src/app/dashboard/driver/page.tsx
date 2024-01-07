"use client";
import "../../globals.css";

import { Button } from "@/app/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/app/components/ui/card";
import {
  Tabs,
  TabsContent,
  TabsList,
  TabsTrigger,
} from "@/app/components/ui/tabs";
import { CalendarDateRangePicker } from "@/app/components/ui/date-range-picker";
import { Search } from "@/app/components/ui/search";
import { UserNav } from "@/app/components/ui/user-nav";
import { useState, useEffect, useCallback } from "react";
import axios from "axios";
import { MainNav } from "../../components/ui/main-nav";
import { DataTable } from "../../components/ui/data-table";
import {
  DriverFilter,
  driverColumns,
} from "../../components/columns/driver-column";
import { CreateDriverForm } from "../../components/forms/createdriver-form";
import {
  Sheet,
  SheetContent,
  SheetDescription,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
} from "../../components/ui/sheet";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import * as z from "zod";
import { useFormConfig, formSchema } from "@/app/components/forms/form-utils";

export default function DriverPage() {
  const [drivers, setDrivers] = useState(null);
  const [driversCount, setDriversCount] = useState(null);

  const form = useFormConfig();
  // Function to fetch data from the C# endpoints
  const fetchData = async () => {
    try {
      const response = await axios.get("http://localhost:54315/api/Driver");
      setDrivers(response.data);
      setDriversCount(response.data.length);
    } catch (error) {
      console.error("Error fetching vehicle data:", error);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);
  return (
    <>
      <div className="hidden flex-col md:flex">
        <div className="border-b">
          <div className="flex h-16 items-center px-4">
            <MainNav className="mx-6" />
            <div className="ml-auto flex items-center space-x-4">
              <Search />
              <UserNav />
            </div>
          </div>
        </div>
        <div className="flex-1 space-y-4 p-8 pt-6">
          <div className="flex items-center justify-between space-y-2">
            <h2 className="text-3xl font-bold tracking-tight">Drivers</h2>
            <div className="flex items-center space-x-2">
              <Sheet>
                <SheetTrigger className="h-9 px-4 py-2 bg-primary text-primary-foreground shadow hover:bg-primary/90 inline-flex items-center justify-center rounded-md text-sm font-medium transition-colors focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring disabled:pointer-events-none disabled:opacity-50">
                  Create Driver
                </SheetTrigger>
                <SheetContent>
                  <SheetHeader>
                    <SheetTitle>Create Driver Form</SheetTitle>
                    <SheetDescription>
                      <CreateDriverForm form={form} formSchema={formSchema} />
                    </SheetDescription>
                  </SheetHeader>
                </SheetContent>
              </Sheet>
            </div>
          </div>
          <Tabs defaultValue="overview" className="space-y-4">
            <TabsContent value="overview" className="space-y-4">
              <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
                <Card>
                  <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                    <CardTitle className="text-sm font-medium">
                      Total Drivers
                    </CardTitle>
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 24 24"
                      fill="none"
                      stroke="currentColor"
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      strokeWidth="2"
                      className="h-4 w-4 text-muted-foreground"
                    >
                      <path d="M16 21v-2a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v2" />
                      <circle cx="9" cy="7" r="4" />
                      <path d="M22 21v-2a4 4 0 0 0-3-3.87M16 3.13a4 4 0 0 1 0 7.75" />
                    </svg>
                  </CardHeader>
                  <CardContent>
                    <div className="text-2xl font-bold">
                      {driversCount ? driversCount : "UNKNOWN"}
                    </div>
                  </CardContent>
                </Card>
              </div>
              <DataTable
                data={drivers || []}
                columns={driverColumns}
                FilterComponent={DriverFilter}
              />
            </TabsContent>
          </Tabs>
        </div>
      </div>
    </>
  );
}
