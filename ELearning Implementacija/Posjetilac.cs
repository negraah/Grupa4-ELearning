using System;

namespace ELearning
{
    public class Posjetilac
    {
        int guestID;

        public Posjetilac(int guestID) => this.guestID = guestID;

        public void prijaviSe()
        {
            // pass
        }

        public void registracija()
        {
            // pass
        }

        public int getGuestID()
        {
            return guestID;
        }

        public void setGuestID(int guestID)
        {
            this.guestID = guestID;
        }

    }
}