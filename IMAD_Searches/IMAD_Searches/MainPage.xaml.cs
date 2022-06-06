namespace IMAD_Searches;

public partial class MainPage : ContentPage
{
    int[] values = { 2, 4, 6, 12, 15, 15, 23, 31, 33, 33, 36, 40, 41, 56, 58, 68, 72, 73, 81, 99 };
    List<Label> labels = new List<Label>();
    int delay = 500;
    int target = 0;
    bool searching = false;

    public MainPage()
	{
		InitializeComponent();
        LoadValues();
	}

    void LoadValues()
    {
        for (int i = 0; i < values.Length; i++)
        {
            Label label = new Label();
            label.Text = values[i].ToString();
            label.BackgroundColor = ColorPicker.notSearched;
            label.HeightRequest = 18;
            label.WidthRequest = 18;
            label.HorizontalTextAlignment = TextAlignment.Center;
            labels.Add(label);
        }

        horizSL.Children.Clear();
        foreach (var label in labels)
        {
            horizSL.Add(label);
        }
    }

    public async void LinearSearch(object sender, EventArgs args)
    {
        if (searching) return;

        searching = true;

        target = Convert.ToInt32(targetEntry.Text);

        for (int i = 0; i < values.Length; i++)
        {
            labels[i].BackgroundColor = ColorPicker.lookingAt;
            await NextStep(delay);

            if (values[i] == target)
            {
                labels[i].BackgroundColor = ColorPicker.found;
                await NextStep(delay);
                searching = false;
                return;
            }
            else
            {
                labels[i].BackgroundColor = ColorPicker.searched;
                await NextStep(delay);
            }
        }
        searching = false;
    }

    public async void BinarySearch(object sender, EventArgs args)
    {
        if(searching) return;

        searching = true;
        target = Convert.ToInt32(targetEntry.Text);

        int first = 0;
        int last = values.Length;

        while(first <= last)
        {
            int mid = (first + last) / 2;
            labels[mid].BackgroundColor = ColorPicker.lookingAt;
            await NextStep(delay);

            if(target == values[mid])
            {
                labels[mid].BackgroundColor = ColorPicker.found;
                await NextStep(delay);
                searching = false;
                return;
            }
            else
            {
                if (values[mid] > target)
                    last = mid - 1;
                else
                    first = mid + 1;
            }
            labels[mid].BackgroundColor = ColorPicker.searched;
            await NextStep(delay);
        }
        searching = false;
    }

    public void ResetValues(object sender, EventArgs args)
    {
        if (searching) return;

        horizSL.Clear();
        labels.Clear();
        LoadValues();
    }

    async Task NextStep(int time)
    {
        horizSL.Children.Clear();
        foreach (var label in labels)
        {
            horizSL.Add(label);
        }
        await Task.Delay(time);
    }

    static class ColorPicker
    {
        public static Color lookingAt = Color.FromArgb("#77FF77");
        public static Color searched = Colors.Black;
        public static Color found = Colors.Cyan;
        public static Color notSearched = Colors.Grey;
    }
}

