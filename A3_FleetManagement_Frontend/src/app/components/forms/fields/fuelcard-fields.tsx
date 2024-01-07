import { useEffect, useState } from "react";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "../../ui/form";
import { Input } from "../../ui/input";
import { FuelCard } from "../form-utils";
import { Popover, PopoverContent, PopoverTrigger } from "../../ui/popover";
import { Button } from "../../ui/button";
import { ChevronsUpDown } from "lucide-react";
import { Command, CommandGroup, CommandItem } from "../../ui/command";

export function FuelCardFields({ control }: { control: any }) {
  const [open, setOpen] = useState(false);
  const [selectedFuelCards, setSelectedFuelCards] = useState<number[]>([]); // Store array of fuelCardIDs
  const [fuelCards, setFuelCards] = useState<FuelCard[]>([]); // Use FuelCardType for state

  // Fetch fuel card types from API
  useEffect(() => {
    fetch("http://localhost:54315/api/FuelCard")
      .then((response) => response.json())
      .then((data: FuelCard[]) => setFuelCards(data)) // Ensure data is of type FuelCardType[]
      .catch((error) =>
        console.error("Error fetching fuel card types:", error)
      );
  }, []);

  // Handler for selecting/deselecting fuel cards
  const handleSelect = (fuelCardID: number) => {
    // Specify type for fuelCardID
    if (selectedFuelCards.includes(fuelCardID)) {
      setSelectedFuelCards(selectedFuelCards.filter((id) => id !== fuelCardID));
    } else {
      setSelectedFuelCards([...selectedFuelCards, fuelCardID]);
    }
  };

  // Create label for the button based on selected fuel cards
  const selectedLabels = fuelCards
    .filter((fuelCard) => selectedFuelCards.includes(fuelCard.fuelCardId))
    .map((fuelCard) => fuelCard.fuelCardId)
    .join(", ");

  return (
    <>
      <FormField
        control={control}
        name="driver.fuelCard.cardNumber"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Card Number</FormLabel>
            <FormControl>
              <Input {...field} />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={control}
        name="driver.fuelCard.expirationDate"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Expiration Date</FormLabel>
            <FormControl>
              <Input
                {...field}
                type="date"
                value={field.value || ""}
                onChange={(e) => field.onChange(e.target.value)}
              />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={control}
        name="driver.fuelCard.pinCode"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Pin Code</FormLabel>
            <FormControl>
              <Input
                {...field}
                value={field.value || ""}
                onChange={(e) => field.onChange(e.target.value)}
              />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={control}
        name="driver.fuelCards"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Fuel Cards</FormLabel>
            <FormControl>
              <div className="w-full">
                <Popover open={open} onOpenChange={setOpen}>
                  <PopoverTrigger asChild>
                    <Button
                      variant="outline"
                      role="combobox"
                      aria-expanded={open}
                      className="w-full justify-between"
                    >
                      {selectedLabels.length > 0
                        ? selectedLabels
                        : "Select fuel cards..."}
                      <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                    </Button>
                  </PopoverTrigger>
                  <PopoverContent className="w-full p-0">
                    <Command>
                      <CommandGroup className="max-h-60 overflow-y-scroll">
                        {fuelCards.map((fuelCard) => (
                          <CommandItem
                            key={fuelCard.fuelCardId}
                            value={fuelCard.fuelCardId.toString()}
                            onSelect={() => handleSelect(fuelCard.fuelCardId)}
                          >
                            {`${fuelCard.fuelCardId} - ${fuelCard.cardNumber}`}
                          </CommandItem>
                        ))}
                      </CommandGroup>
                    </Command>
                  </PopoverContent>
                </Popover>
              </div>
            </FormControl>
          </FormItem>
        )}
      />
    </>
  );
}
