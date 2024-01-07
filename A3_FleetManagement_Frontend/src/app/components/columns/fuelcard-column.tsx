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

type FuelCard = {
  fuelCardId: number;
  cardNumber: string;
  expirationDate: string;
  pinCode: string;
  blocked: boolean;
  acceptedFuels: string[];
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

export function FuelCardFilter({ table }: FilterProps) {
  return (
    // Your driver-specific filter logic and UI here
    <Input
      placeholder="Filter Card Number..."
      value={(table.getColumn("cardNumber")?.getFilterValue() as string) ?? ""}
      onChange={(event) =>
        table.getColumn("cardNumber")?.setFilterValue(event.target.value)
      }
      className="max-w-sm"
    />
  );
}

export const fuelCardColumns: ColumnDef<FuelCard>[] = [
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
    accessorKey: "fuelCardId",
    header: ({ column }) => createSortingHeader(column, "Id"),
    cell: ({ row }) => (
      <div className="text-left p-2">{row.getValue("fuelCardId")}</div>
    ),
  },
  {
    accessorKey: "cardNumber",
    header: ({ column }) => createSortingHeader(column, "Card Number"),
    cell: ({ row }) => (
      <div className="text-left p-2">{row.getValue("cardNumber")}</div>
    ),
  },
  {
    accessorKey: "expirationDate",
    header: ({ column }) => createSortingHeader(column, "Expiration Date"),
    cell: ({ row }) => (
      <div className="text-left p-2">
        {new Date(row.getValue("expirationDate")).toLocaleDateString()}
      </div>
    ),
  },
  {
    accessorKey: "pinCode",
    header: ({ column }) => createSortingHeader(column, "Pin Code"),
    cell: ({ row }) => (
      <div className="text-left p-2">{row.getValue("pinCode")}</div>
    ),
  },
  {
    accessorKey: "blocked",
    header: ({ column }) => createSortingHeader(column, "Blocked"),
    cell: ({ row }) => (
      <div className="text-left p-2">
        {row.getValue("blocked") ? "Yes" : "No"}
      </div>
    ),
  },
  {
    id: "actions",
    cell: ({ row }) => {
      const fuelCard = row.original;

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
                  navigator.clipboard.writeText(fuelCard.fuelCardId.toString())
                }
              >
                Copy fuel card ID
              </DropdownMenuItem>
              <DropdownMenuSeparator />
              <DropdownMenuItem>View fuel card</DropdownMenuItem>
              <DropdownMenuItem>View fuel card details</DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
      );
    },
  },
];
