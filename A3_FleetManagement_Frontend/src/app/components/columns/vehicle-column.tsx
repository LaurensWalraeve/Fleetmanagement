"use client";

import { ColumnDef } from "@tanstack/react-table";
import { ArrowUpDown, MoreHorizontal } from "lucide-react";

import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "../ui/dropdown-menu";

import { Button } from "../ui/button";
import { Checkbox } from "../ui/checkbox";
import { Input } from "../ui/input";

type Vehicle = {
  vehicleId: number;
  make: string;
  model: string;
  fuelType: string | null;
  vehicleType: string | null;
  chassisNumber: string;
  licensePlate: string;
  color: string;
  numberOfDoors: number;
};

type SortDirection = "asc" | "desc";

type ColumnType = {
  toggleSorting: (condition: boolean) => void;
  getIsSorted: () => false | SortDirection;
};

function createSortingHeader(column: ColumnType, title: string) {
  return (
    <Button
      className="text-left p-2"
      variant="ghost"
      onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
    >
      {title}
      <ArrowUpDown className="ml-2 h-4 w-4" />
    </Button>
  );
}

interface FilterProps {
  table: any; // You can replace 'any' with the appropriate type if available
}

export function VehicleFilter({ table }: FilterProps) {
  return (
    // Your driver-specific filter logic and UI here
    <Input
      placeholder="Filter Chassis Number..."
      value={
        (table.getColumn("chassisNumber")?.getFilterValue() as string) ?? ""
      }
      onChange={(event) =>
        table.getColumn("chassisNumber")?.setFilterValue(event.target.value)
      }
      className="max-w-sm"
    />
  );
}

export const vehicleColumns: ColumnDef<Vehicle>[] = [
  {
    id: "select",
    header: ({ table }) => (
      <Checkbox
        checked={table.getIsAllPageRowsSelected()}
        onCheckedChange={(value) => table.toggleAllPageRowsSelected(!!value)}
        aria-label="Select all"
      />
    ),
    cell: ({ row }) => (
      <Checkbox
        checked={row.getIsSelected()}
        onCheckedChange={(value) => row.toggleSelected(!!value)}
        aria-label="Select row"
      />
    ),
    enableSorting: false,
    enableHiding: false,
  },
  {
    accessorKey: "vehicleId",
    header: ({ column }) => createSortingHeader(column, "Id"),
    cell: ({ row }) => (
      <div className="text-left p-2">{row.getValue("vehicleId")}</div>
    ),
  },
  {
    accessorKey: "make",
    header: ({ column }) => createSortingHeader(column, "Make"),
    cell: ({ row }) => (
      <div className="text-left p-2">{row.getValue("make")}</div>
    ),
  },
  {
    accessorKey: "model",
    header: ({ column }) => createSortingHeader(column, "Model"),
    cell: ({ row }) => (
      <div className="text-left p-2">{row.getValue("model")}</div>
    ),
  },
  {
    accessorKey: "chassisNumber",
    header: ({ column }) => createSortingHeader(column, "Chassis Number"),
    cell: ({ row }) => (
      <div className="text-left p-2">{row.getValue("chassisNumber")}</div>
    ),
  },
  {
    accessorKey: "licensePlate",
    header: ({ column }) => createSortingHeader(column, "License Plate"),
    cell: ({ row }) => (
      <div className="text-left p-2">{row.getValue("licensePlate")}</div>
    ),
  },
  {
    accessorKey: "color",
    header: ({ column }) => createSortingHeader(column, "Color"),
    cell: ({ row }) => (
      <div className="text-left p-2">{row.getValue("color")}</div>
    ),
  },
  {
    accessorKey: "numberOfDoors",
    header: ({ column }) => createSortingHeader(column, "Number of Doors"),
    cell: ({ row }) => (
      <div className="text-left p-2">{row.getValue("numberOfDoors")}</div>
    ),
  },
  {
    id: "actions",
    cell: ({ row }) => {
      const vehicle = row.original;

      return (
        <div className="flex justify-end">
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="ghost" className="h-8 w-8 p-0">
                <span className="sr-only">Open menu</span>
                <MoreHorizontal className="h-4 w-4" />
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
              <DropdownMenuLabel>Actions</DropdownMenuLabel>
              <DropdownMenuItem
                onClick={() =>
                  navigator.clipboard.writeText(vehicle.vehicleId.toString())
                }
              >
                Copy vehicle ID
              </DropdownMenuItem>
              <DropdownMenuSeparator />
              <DropdownMenuItem>View vehicle</DropdownMenuItem>
              <DropdownMenuItem>View vehicle details</DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
      );
    },
  },
];
