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
import { Tabs, TabsContent } from "@/app/components/ui/tabs";
import { CalendarDateRangePicker } from "@/app/components/ui/date-range-picker";
import { Overview } from "@/app/components/ui/overview";
import { RecentFuelCardUse } from "@/app/components/ui/recent-sales";
import { UserNav } from "@/app/components/ui/user-nav";
import { useState, useEffect, useCallback } from "react";
import axios from "axios";
import { MainNav } from "../../components/ui/main-nav";
import { userColumns } from "@/app/components/columns/user-column";
import {
  Table,
  TableBody,
  TableCaption,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/app/components/ui/table";

export default function DashboardPage() {
  const [totalFuelCardAmount, setTotalFuelCardAmount] = useState(null);
  const [user, setUsers] = useState(null);

  // Function to fetch data from the C# endpoints
  const fetchData = async () => {
    try {
      const responseFuelCards = await axios.get(
        "http://localhost:54315/api/FuelCard/Count"
      );

      setTotalFuelCardAmount(responseFuelCards.data);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  useEffect(() => {
    // Fetch data when the component mounts
    fetchData();
  }, []);
  return (
    <>
      <div className="hidden flex-col md:flex">
        <div className="border-b">
          <div className="flex h-16 items-center px-4">
            <div className="ml-auto flex items-center space-x-4">
              <UserNav />
            </div>
          </div>
        </div>
        <div className="flex-1 space-y-4 p-8 pt-6">
          <div className="flex items-center justify-between space-y-2">
            <h2 className="text-3xl font-bold tracking-tight">Dashboard</h2>
          </div>
          <Tabs defaultValue="overview" className="space-y-4">
            <TabsContent value="overview" className="space-y-4">
              <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
                <Card>
                  <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                    <CardTitle className="text-sm font-medium">
                      Total Fuel Card Use
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
                      <path d="M12 2v20M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6" />
                    </svg>
                  </CardHeader>
                </Card>
              </div>
              <div className="grid gap-2 md:grid-cols-2 lg:grid-cols-7">
                <Card className="col-span-3">
                  <CardHeader>
                    <CardTitle>Recent Fuel Card Use</CardTitle>
                    <CardDescription></CardDescription>
                  </CardHeader>
                  <CardContent>
                    <RecentFuelCardUse />
                  </CardContent>
                </Card>
                <div className="col-span-4">
                  <Table>
                    <Card className="mb-[2%]">
                      <Table>
                        <TableHeader>
                          <TableRow>
                            <TableHead className="w-[100px">
                              Driver ID
                            </TableHead>
                            <TableHead>Last Name</TableHead>
                            <TableHead>First Name</TableHead>
                            <TableHead>Birth Date</TableHead>
                            <TableHead>Social Security Number</TableHead>
                            <TableHead>License Type</TableHead>
                            <TableHead>Address ID</TableHead>
                            <TableHead>Street</TableHead>
                            <TableHead>House Number</TableHead>
                            <TableHead>Zip Code</TableHead>
                            <TableHead>City</TableHead>
                            <TableHead>Vehicle</TableHead>
                            <TableHead>Fuel Card</TableHead>
                          </TableRow>
                        </TableHeader>
                        <TableBody>
                          <TableRow>
                            <TableCell>1</TableCell>
                            <TableCell>Doe</TableCell>
                            <TableCell>John</TableCell>
                            <TableCell>01/15/1985</TableCell>
                            <TableCell>123-45-6789</TableCell>
                            <TableCell>Class A</TableCell>
                            <TableCell>1</TableCell>
                            <TableCell>123 Main St</TableCell>
                            <TableCell>456</TableCell>
                            <TableCell>12345</TableCell>
                            <TableCell>New York</TableCell>
                            <TableCell>Vehicle Data</TableCell>
                            <TableCell>Fuel Card Data</TableCell>
                          </TableRow>
                        </TableBody>
                      </Table>
                    </Card>
                    <Card className="mb-[2%]">
                      <TableHeader>
                        <TableRow>
                          <TableHead className="w-[100px]">
                            Vehicle ID
                          </TableHead>
                          <TableHead>Make</TableHead>
                          <TableHead>Model</TableHead>
                          <TableHead>Chassis Number</TableHead>
                          <TableHead>License Plate</TableHead>
                          <TableHead>Color</TableHead>
                        </TableRow>
                      </TableHeader>
                      <TableBody>
                        <TableRow>
                          <TableCell>1</TableCell>
                          <TableCell>Toyota</TableCell>
                          <TableCell>Camry</TableCell>
                          <TableCell>ABC123456</TableCell>
                          <TableCell>XYZ-987</TableCell>
                          <TableCell>Blue</TableCell>
                        </TableRow>
                      </TableBody>
                    </Card>
                    <Card className="mb-[2%]">
                      <TableHeader>
                        <TableRow>
                          <TableHead className="w-[100px]">
                            Fuel Card ID
                          </TableHead>
                          <TableHead>Card Number</TableHead>
                          <TableHead>Expiration Date</TableHead>
                        </TableRow>
                      </TableHeader>
                      <TableBody>
                        <TableRow>
                          <TableCell>1</TableCell>
                          <TableCell>1234 5678 9012 3456</TableCell>
                          <TableCell>12/25</TableCell>
                        </TableRow>
                      </TableBody>
                    </Card>
                  </Table>{" "}
                </div>
              </div>
            </TabsContent>
          </Tabs>
        </div>
      </div>
    </>
  );
}
